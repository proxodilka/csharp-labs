using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace blab9
{
    class Program
    {
        static void ReportPerformance(int test_arr_size, string distribution)
        {
            int[] arr = Utils.GetRandomArray(test_arr_size, distribution);

            int[] threads_to_bench = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 16, 24, 32, 48, 64, 128 };

            Bench.BenchSort(
                Sort.ParallelQuadricSort,
                arr,
                threads_to_bench,
                /*repeats=*/5,
                /*filename=*/$"quadric_{distribution}_{test_arr_size}.csv"
            );
            Bench.BenchSort(
                Sort.ParallelQuickQuadricSort,
                arr,
                threads_to_bench,
                /*repeats=*/5,
                /*filename=*/$"quick_quadric_{distribution}_{test_arr_size}.csv"
            );
        }

        static void Main(string[] args)
        {
            int N = 100000;
            ReportPerformance(N, "uniform");
            ReportPerformance(N, "normal");
        }
    }
}
