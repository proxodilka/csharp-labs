using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace blab9
{
    class Range
    {
        public int begin;
        public int end;
        public bool IsValid { get { return begin < end; } }
    }
    class Sort
    {
        const int INF = int.MaxValue;
        const int MINF = int.MinValue;
        public static int verbose = 0;
        static void QuadricSort(object sort_params)
        {
            object[] parameters = (sort_params as object[]);
            int[] arr = (parameters[0] as int[]);
            Range range = (Range)parameters[1];
            QuadricSort(arr, range);
        }

        public static void QuadricSort(int[] arr, Range range)
        {
            QuadricSort(arr, range.begin, range.end);
        }
        public static void QuadricSort(int[] arr, int begin=0, int end=-1)
        {
            if (end == -1) { end = arr.Length; }
            for (int i=begin; i<end; i++)
            {
                for (int j=i+1; j<end; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        int tmp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = tmp;
                    }
                }
            }
        }

        public static void Merge(ref int[] arr, Range[] ranges)
        {
            int result_length = 0;
            foreach (var range in ranges)
            {
                result_length += range.end - range.begin;
            }
            int[] result = new int[result_length];

            for (int i=0; i<result_length; i++)
            {
                int idx_to_append = -1, min_value = INF;
                for (int j=0; j<ranges.Length; j++)
                {
                    if (ranges[j].IsValid && arr[ranges[j].begin] < min_value)
                    {
                        idx_to_append = j;
                        min_value = arr[ranges[j].begin];
                    }
                }
                result[i] = arr[ranges[idx_to_append].begin];
                ranges[idx_to_append].begin++;
            }
            arr = result;
        }

        public static void Stack(int[][] values, int[] output)
        {
            int i = 0;
            foreach(var ls in values)
            {
                foreach (int x in ls)
                {
                    output[i++] = x;
                }
            }
        }

        public static void ParallelQuadricSort(ref int[] arr, int? nthreads_ = null)
        {
            int nthreads = GetNthreads(nthreads_);
            int chunk_size = (int)Math.Ceiling(arr.Length / (double)nthreads);
            List<Thread> threads = new List<Thread>(nthreads);
            Range[] ranges = new Range[nthreads];
            for (int i=0, k=0; i < arr.Length; i += chunk_size, k++)
            {
                threads.Add(new Thread(QuadricSort));
                ranges[k] = new Range() { 
                    begin=i,
                    end = Math.Min(i + chunk_size, arr.Length) 
                };
                threads[k].Start(new object[] { arr, ranges[k]});
            }
            if (ranges[ranges.Length - 1] == null)
            {
                ranges[ranges.Length - 1] = new Range() { begin=0, end=0};
            }
            foreach(var thr in threads)
            {
                thr.Join();
            }
            Merge(ref arr, ranges);
        }

        public static void ParallelQuickQuadricSort(ref int[] arr, int? nthreads_=null)
        {
            int nthreads = GetNthreads(nthreads_);
            int max = MINF, min = INF;
            foreach (int x in arr)
            {
                if (max < x)
                {
                    max = x;
                }
                if (min > x)
                {
                    min = x;
                }
            }

            int chunk_size = (int)((max - min) / (double)nthreads + 0.5);

            List<List<int>> slices_ = new List<List<int>>(nthreads);

            for (int i = 0; i < nthreads; i++)
            {
                slices_.Add(new List<int>());
            }

            foreach (int x in arr)
            {
                int thread_id = Math.Min((x - min) / chunk_size, slices_.Count - 1);
                if (verbose > 1)
                {
                    Console.WriteLine($"{x} was dispatched at Thread{thread_id}");
                }
                slices_[thread_id].Add(x);
            }

            if (verbose > 0)
            {
                Console.WriteLine("Threads load:");
                for (int i=0; i<nthreads; i++)
                {
                    Console.WriteLine($"\t Thread{i}: {(slices_[i].Count / (double)arr.Length) * 100}%");
                }
            }

            List<Thread> threads = new List<Thread>(nthreads);
            int[][] slices = new int[nthreads][];

            for (int i=0; i<nthreads; i++)
            {
                slices[i] = slices_[i].ToArray();
            }

            for (int i=0; i<nthreads; i++)
            {
                threads.Add(
                    new Thread(
                        (n) => QuadricSort(slices[(int)n])
                    )
                );
                threads[threads.Count - 1].Start(i);
            }
            foreach (var thrd in threads)
            {
                thrd.Join();
            }
            Stack(slices, arr);
        }

        static int GetNthreads(int? nthreads_)
        {
            return (nthreads_ != null) ? (int)nthreads_ : Environment.ProcessorCount;
        }
    }
}
