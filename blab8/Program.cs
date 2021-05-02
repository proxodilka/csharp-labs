using System;
using System.Threading;

namespace blab8
{
    class Program
    {
        static ManualResetEvent dep1 = new ManualResetEvent(false);
        static ManualResetEvent dep2 = new ManualResetEvent(false);
        static ManualResetEvent dep3 = new ManualResetEvent(false);
        static ManualResetEvent dep4 = new ManualResetEvent(false);
        static ManualResetEvent dep5 = new ManualResetEvent(false);
        static ManualResetEvent dep6 = new ManualResetEvent(false);

        static double a = 1, b = 5, c = 6;
        static double _b, b2, ac4, d, dsqrt, a2, x1, x2;

        static void _B()
        {
            _b = -b;
            dep2.Set();
            Console.WriteLine("_B done");
        }

        static void B2()
        {
            b2 = b * b;
            dep3.Set();
            Console.WriteLine("B2 done");
        }
        static void AC4()
        {
            ac4 = 4 * a * c;
            dep4.Set();
            Console.WriteLine("AC4 done");
        }
        static void D()
        {
            EventWaitHandle.WaitAll(new EventWaitHandle[] { dep3, dep4 });
            d = b2 - ac4;
            dep5.Set();
            Console.WriteLine("D done");
        }
        static void DSQRT()
        {
            dep5.WaitOne();
            dsqrt = Math.Sqrt(d);
            dep6.Set();
            Console.WriteLine("DSQRT done");
        }
        static void A2()
        {
            a2 = 2 * a;
            dep1.Set();
            Console.WriteLine("A2 done");
        }
        static void X1()
        {
            EventWaitHandle.WaitAll(new EventWaitHandle[] { dep1, dep2, dep6 });
            x1 = (_b + dsqrt) / a2;
            Console.WriteLine("X1 done");
        }
        static void X2()
        {
            EventWaitHandle.WaitAll(new EventWaitHandle[] { dep1, dep2, dep6 });
            x2 = (_b - dsqrt) / a2;
            Console.WriteLine("X2 done");
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"Solving {a}*x^2 + {b}*x + {c} = 0");
            Thread[] threads = new Thread[] {
                new Thread(_B), new Thread(A2), new Thread(B2), new Thread(AC4),
                new Thread(D), new Thread(DSQRT), new Thread(X1), new Thread(X2)
            };
            foreach (var thr in threads)
            {
                thr.Start();
            }
            foreach (var thr in threads)
            {
                thr.Join();
            }
            Console.WriteLine($"x1 = {x1}; x2 = {x2}");
        }
    }
}
