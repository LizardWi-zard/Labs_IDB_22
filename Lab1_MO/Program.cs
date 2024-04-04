using System;

class FibonacciMethod
{
    static double F(double x) => 2 * Math.Pow(x, 3) + 9 * Math.Pow(x, 2) - 21;

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

    static double FibonacciSearch(double a, double b, double e)
    {
        var N = FindN(a, b, e);
        var k = 0;

        var y = a + (double)Fibonacci(N - 2) / Fibonacci(N) * (b - a);
        var z = a + (double)Fibonacci(N - 1) / Fibonacci(N) * (b - a);

        var fy = F(y);
        var fz = F(z);

        Output(k, N, a, b, y, z, fy, fz);

        while (k != N - 3)
        {
            if (fy <= fz)
            {
                b = z;
                z = y;
                y = a + (double)Fibonacci(N - k - 3) / Fibonacci(N - k - 1) * (b - a);
                fz = fy;
                fy = F(y);
            }
            else
            {
                a = y;
                y = z;
                z = a + (double)Fibonacci(N - k - 2) / Fibonacci(N - k - 1) * (b - a);
                fy = fz;
                fz = F(z);
            }

            k++;

            Output(k, N, a, b, y, z, fy, fz);
        }

        var yN = (a + b) / 2;
        var zN = yN;

        if (k == N - 3)
        {
            yN = zN = (a + b) / 2;
            zN = yN + e;
        }

        var fyN = F(yN);
        var fzN = F(zN);

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

    static void Main(string[] args)
    {
        double a = -1; 
        double b = 3;
        double l = 0.3;
        double e = 0.3;

        double min = FibonacciSearch(a, b, l);

        Console.WriteLine($"Минимум функции на интервале [{a}, {b}] с длиной интервала {l} и константой различимости {e} равен {min}");
        Console.WriteLine("Значение функции в найденной точки:" + F(min));
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
