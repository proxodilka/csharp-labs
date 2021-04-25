using System;
using System.Collections.Generic;
using System.Threading;

namespace blab7
{
  class SumArray
  {
    static object locker = new object();

    public static int Sum(IEnumerable<int> arr)
    {
      int result;
      // lock (locker)
      {
        result = 0;
        foreach (int x in arr)
        {
          result += x;
          Console.WriteLine($"Текущая сумма для потока {Thread.CurrentThread.Name} равна {result}");
          Thread.Sleep(100);
        }
      }
      return result;
    }
  }

  class SumThread
  {
    public Thread thread;
    IEnumerable<int> arr;
    int ans;

    public SumThread(string name, IEnumerable<int> arr)
    {
      this.arr = arr;
      ans = 0;

      thread = new Thread(this.Run) { Name = name };
      thread.Start();
    }

    void Run()
    {
      Console.WriteLine($"{Thread.CurrentThread.Name} начат...");
      ans = SumArray.Sum(arr);
      Console.WriteLine($"Сумма для {Thread.CurrentThread.Name} равна {ans}");
    }
  }
}
