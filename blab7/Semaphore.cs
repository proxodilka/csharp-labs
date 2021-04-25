using System;
using System.Threading;

namespace blab7
{
  class CharPrinter
  {
    static Semaphore semaphore = new Semaphore(2, 2);

    public Thread thread;
    public CharPrinter(string name)
    {
      thread = new Thread(this.Run) { Name = name };
      thread.Start();
    }

    void Run()
    {
      Console.WriteLine($"{Thread.CurrentThread.Name} ожидает разрешения на вход в крит. секцию...");
      semaphore.WaitOne();
      Console.WriteLine($"{Thread.CurrentThread.Name} вошёл в крит. секцию");

      for (char i = 'A'; i < 'G'; i++)
      {
        Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
        Thread.Sleep(100);
      }

      Console.WriteLine($"{Thread.CurrentThread.Name} освобождает секцию");
      semaphore.Release();
    }
  }
}
