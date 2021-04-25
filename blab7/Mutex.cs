using System;
using System.Threading;

namespace blab7
{
  class SharedRes
  {
    public static int value;
    public static Mutex mutex = new Mutex();
  }
  abstract class ThreadWorker
  {
    protected int num;
    public Thread thread;

    public ThreadWorker(string name, int num)
    {
      this.num = num;

      thread = new Thread(this.Run) { Name = name };
      thread.Start();
    }

    void Run()
    {
      Console.WriteLine($"{Thread.CurrentThread.Name} ожидает мьютекс...");
      SharedRes.mutex.WaitOne();
      Console.WriteLine($"{Thread.CurrentThread.Name} получил мьютекс");

      do
      {
        Thread.Sleep(100);
        DoWork(ref SharedRes.value);
        Console.WriteLine($"В {Thread.CurrentThread.Name}е SharedRes.value = {SharedRes.value}");
      } while (num > 0);

      SharedRes.mutex.ReleaseMutex();
    }

    protected abstract void DoWork(ref int value);
  }
  class Incrementor : ThreadWorker
  {
    public Incrementor(string name, int n) : base(name, n) { }
    protected override void DoWork(ref int value)
    {
      value++;
      num--;
    }
  }
  class Decrementor : ThreadWorker
  {
    public Decrementor(string name, int n) : base(name, n) { }
    protected override void DoWork(ref int value)
    {
      value--;
      num--;
    }
  }
}
