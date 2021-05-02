using System;
using System.Collections.Generic;
using System.Threading;

namespace blab7
{
  class SumArray
  {
    object locker = new object();
    int result = 0;
    public int Sum(IEnumerable<int> arr)
    {
      int result;
      result = 0;
      foreach (int x in arr)
      {
          result += x;
          Thread.Sleep(new Random().Next(10, 100));
      }
      Console.WriteLine($"{Thread.CurrentThread.Name} посчитал свою часть суммы, ожидает разрешения на вход в крит. секцию...");
      lock (locker)
      {
        Console.WriteLine($"{Thread.CurrentThread.Name} вошел в крит. секцию");
        this.result += result;
        Console.WriteLine($"{Thread.CurrentThread.Name} освобождает крит. секцию...");
        Thread.Sleep(100);
      }
      return result;
    }
  }

  class ParallelSum
  {
    List<Thread> threads;
    IEnumerable<int> arr;
    int global_res;
    int report_freq;

    object locker = new object();

    public int Answer { get { return global_res; } }

    public ParallelSum(int[] arr, int report_frequency=0, int nthreads=0)
    {
      this.arr = arr;
      this.report_freq = report_frequency;
      global_res = 0;

      if (nthreads == 0)
        nthreads = Environment.ProcessorCount;


      threads = new List<Thread>(nthreads);
      int chunk_size = (int)(arr.Length / (double)nthreads + 0.5);
      for (int i=0; i<arr.Length; i+=chunk_size)
      {
        threads.Add(
          new Thread(
            (n) => Compute(
              new ArraySegment<int>(
                arr,
                (int)n * chunk_size,
                Math.Min(chunk_size, arr.Length - (int)n * chunk_size)
              )
            )
          ) { Name = $"Thread {threads.Count}"}
        );
      }
    }

    public int Run()
    {
      global_res = 0;
      for (int i=0; i<threads.Count; i++)
      {
        threads[i].Start(i);
      }
      for (int i = 0; i < threads.Count; i++)
      {
        threads[i].Join();
      }
      return global_res;
    }

    void Report(ref int thread_res)
    {
      Console.WriteLine($"{Thread.CurrentThread.Name} is going to report its result, waiting for permission to enter into critical section...");
      lock (locker)
      {
        Console.WriteLine($"{Thread.CurrentThread.Name} has just entered the critical section");
        global_res += thread_res;
        thread_res = 0;
        Console.WriteLine($"{Thread.CurrentThread.Name} leaving critical section...");
        Thread.Sleep(100);
      }
    }

    void Compute(IEnumerable<int> arr)
    {
      int i = 0, thread_res = 0;
      foreach (int x in arr)
      {
        thread_res += x;
        i++;
        Thread.Sleep(10);
        if (thread_res != 0 && report_freq != 0 && i % report_freq == 0)
        {
          Report(ref thread_res);
        }
      }
      if (thread_res != 0)
      {
        Report(ref thread_res);
      }
    }
  }
}
