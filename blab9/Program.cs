using System;
using System.Diagnostics;

namespace blab9
{
    class Program
    {
        static Random rnd = new Random();
        static int[] GetRandomArray(int size)
        {
            int[] result = new int[size];
            for (int i=0; i<size; i++)
            {
                result[i] = rnd.Next();
            }
            return result;
        }
        static void Main(string[] args)
        {
            int[] arr = GetRandomArray(100000);

            Stopwatch sw = new Stopwatch();

            sw.Start();
            var res = Sort.ParallelQuadricSort(arr);
            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds / (double)1000}s");
        }
    }
}
