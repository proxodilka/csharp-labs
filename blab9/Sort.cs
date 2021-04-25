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

        public static int[] Merge(int[] arr, List<Range> ranges)
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
                for (int j=0; j<ranges.Count; j++)
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

            return result;
        }

        public static int[] ParallelQuadricSort(int[] arr, int? nthreads_ = null)
        {
            int nthreads = GetNthreads(nthreads_);
            int chunk_size = (int)(arr.Length / (double)nthreads + 0.5);
            List<Thread> threads = new List<Thread>(nthreads);
            List<Range> ranges = new List<Range>(nthreads);
            for (int i=0; i < arr.Length; i += chunk_size)
            {
                threads.Add(new Thread(QuadricSort));
                ranges.Add(new Range() { 
                    begin=i,
                    end = Math.Min(i + chunk_size, arr.Length) 
                });
                threads[threads.Count - 1].Start(new object[] { arr, ranges[ranges.Count - 1]});
            }
            foreach(var thr in threads)
            {
                thr.Join();
            }
            return Merge(arr, ranges);
        }

        //public static int[] ParallelMergeSort(int[] arr, int? nthreads_ = null)
        //{
            
        //}

        static int GetNthreads(int? nthreads_)
        {
            return (nthreads_ != null) ? (int)nthreads_ : Environment.ProcessorCount;
        }
    }
}
