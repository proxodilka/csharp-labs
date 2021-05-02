using System;
using System.Collections.Generic;
using System.Text;

namespace blab9
{
    class Utils
    {
        public static double NextGaussian(Random r, double mu = 0, double sigma = 1)
        {
            var u1 = r.NextDouble();
            var u2 = r.NextDouble();

            var rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);

            var rand_normal = mu + sigma * rand_std_normal;

            return rand_normal;
        }

        static Random rnd = new Random();

        public static int[] GetRandomArray(int size, string how = "uniform")
        {
            int[] result = new int[size];
            if (how == "monotonic")
            {
                for (int i = 0; i < size; i++)
                {
                    result[i] = size - i;
                }
                return result;
            }
            else if (how == "uniform")
            {
                for (int i = 0; i < size; i++)
                {
                    result[i] = rnd.Next();
                }
                return result;
            }
            else if (how == "normal")
            {
                for (int i = 0; i < size; i++)
                {
                    result[i] = (
                        (int)NextGaussian(rnd, 0, 100)
                    );
                }
                return result;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        static bool IsSorted(IList<int> arr)
        {
            for (int i = 1; i < arr.Count; i++)
            {
                if (arr[i] < arr[i - 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
