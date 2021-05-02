using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.IO;

namespace blab9
{
    class Bench
    {
        public delegate void SortDelegate(ref int[] arr, int? nthreads_);
        public static void BenchSort(
            SortDelegate func,
            int[] src,
            int[] threads_arr,
            int nrepeats = 5,
            string filename = "output.csv"
        )
        {
            List<Tuple<long, long>> results = new List<Tuple<long, long>>();
            int[] src_ = new int[src.Length];

            foreach (var nthreads in threads_arr)
            {
                for (int j = 0; j < nrepeats; j++)
                {
                    for (int i = 0; i < src.Length; i++)
                    {
                        src_[i] = src[i];
                    }
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    func(ref src_, nthreads);
                    sw.Stop();
                    results.Add(new Tuple<long, long>(nthreads, sw.ElapsedMilliseconds));
                    Console.WriteLine($"{nthreads} : {sw.ElapsedMilliseconds} : {j + 1}/{nrepeats}");
                }
            }

            var swr = new StreamWriter(filename);
            swr.WriteLine("nthreads,elapsed_ms");
            foreach (var res in results)
            {
                swr.WriteLine($"{res.Item1},{res.Item2}");
            }
            swr.Close();
        }
    }
}
