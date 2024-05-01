using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MO
{
    static class FibonacciMethod
    {
        static double PhiFunction((double, double) point, (double, double) gradient, double t) => 6 * Math.Pow(point.Item1 - t * gradient.Item1, 2) + 0.4 * (point.Item1 - t * gradient.Item1) * (point.Item2 - t * gradient.Item2) + 5 * Math.Pow(point.Item2 - t * gradient.Item2, 2);

        static int Fibonacci(int n)
        {
            if (n <= 1)
            {
                return n;
            }

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        static int FindN(double a, double b, double l)
        {
            var length = b - a;
            var N = 0;

            while (Fibonacci(N) < length / l)
                N++;
            return N;
        }

        static double FibonacciSearch(double a, double b, double e, (double, double) point, (double, double) gradient)
        {
            var N = FindN(a, b, e);
            var k = 0;

            var y = a + (double)Fibonacci(N - 2) / Fibonacci(N) * (b - a);
            var z = a + (double)Fibonacci(N - 1) / Fibonacci(N) * (b - a);

            var fy = PhiFunction(point, gradient, y);
            var fz = PhiFunction(point, gradient, z);

            while (k != N - 3)
            {
                if (fy <= fz)
                {
                    b = z;
                    z = y;
                    y = a + (double)Fibonacci(N - k - 3) / Fibonacci(N - k - 1) * (b - a);
                    fz = fy;
                    fy = PhiFunction(point, gradient, y);
                }
                else
                {
                    a = y;
                    y = z;
                    z = a + (double)Fibonacci(N - k - 2) / Fibonacci(N - k - 1) * (b - a);
                    fy = fz;
                    fz = PhiFunction(point, gradient, z);
                }

                k++;

            }

            var yN = (a + b) / 2;
            var zN = yN;

            if (k == N - 3)
            {
                yN = zN = (a + b) / 2;
                zN = yN + e;
            }

            var fyN = PhiFunction(point, gradient, yN);
            var fzN = PhiFunction(point, gradient, zN);

            if (fyN <= fzN)
            {
                b = zN;
            }
            else
            {
                a = yN;
            }

            return (a + b) / 2;
        }

        static public double GetT(double lowerBound, double upperBound, double tolerance, (double, double) point, (double, double) gradient)
        {
            double a = lowerBound;
            double b = upperBound;
            double l = 0.3;
            double e = tolerance;

            return FibonacciSearch(a, b, e, point, gradient);

        }

        static void Output(int k, int N, double a, double b, double y, double z, double fy, double fz)
        {
            Console.WriteLine($"\niteration {k}:");
            Console.WriteLine("a: " + Math.Round(a, 6) + "\n" +
                              "b: " + Math.Round(b, 6) + "\n" +
                              "N: " + N + "\n" +
                              "y: " + Math.Round(y, 6) + "\n" +
                              "z: " + Math.Round(z, 6) + "\n" +
                              "fy: " + Math.Round(fy, 6) + "\n" +
                              "fz: " + Math.Round(fz, 6) + "\n");
        }
    }
}
