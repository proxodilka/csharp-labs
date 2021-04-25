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
      int[] a = { 1, 2, 3, 4, 5 };
      SumThread mt1 = new SumThread("Поток 1", a);
      SumThread mt2 = new SumThread("Поток 2", a);
      mt1.thread.Join();
      mt2.thread.Join();
      Console.WriteLine("Основной поток завершил выполнение");
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
      // RunMutexExample();
      // RunSemaphoreExample();
    }

  }
}
