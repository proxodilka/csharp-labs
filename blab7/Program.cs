using System;
using System.Threading;

namespace blab7
{
  class Program
  {
    static void RunMutexExample()
    {
      Console.WriteLine(GetSeparator("Mutex"));
      ThreadWorker mt1 = new Incrementor("Инкрементирующий поток", 5);
      ThreadWorker mt2 = new Decrementor("Декрементирующий поток", 5);
      mt1.thread.Join();
      mt2.thread.Join();
      Console.WriteLine("Основной поток завершил выполнение");
    }
    
    static void RunLockExample()
    {
      Console.WriteLine(GetSeparator("Lock"));
      int N = 1000;
      int[] arr = new int[N];

      for (int i=0; i< N; i++)
      {
        arr[i] = i;
      }
      ParallelSum ps = new ParallelSum(arr, 240, 4);
      int res = ps.Run();
      Console.WriteLine($"Основной поток завершил выполнение: {res}");
    }

    static void RunSemaphoreExample()
    {
      Console.WriteLine(GetSeparator("Semaphore"));
      CharPrinter[] cps = new CharPrinter[] {
        new CharPrinter("Поток 1"),
        new CharPrinter("Поток 2"),
        new CharPrinter("Поток 3"),
        new CharPrinter("Поток 4"),
      };
      
      foreach (CharPrinter cp in cps)
      {
        cp.thread.Join();
      }
      Console.WriteLine("Основной поток завершил выполнение");
    }

    static string GetSeparator(string name, int count=10, char sep='-')
    {
      return new string(sep, count) + name + new string(sep, count);
    }
    static void Main(string[] args)
    {
      RunLockExample();
      RunMutexExample();
      RunSemaphoreExample();
    }

  }
}
