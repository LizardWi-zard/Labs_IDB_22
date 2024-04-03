using System;

class FibonacciMethod
{
    static double Function(double x)
    {
        var v = 2 * x * x * x + 9 * x * x - 21; //2 * Math.Pow(x, 3) + 9 * Math.Pow(x, 2) - 21;

        return v; // Пример: квадратичная функция
    }

    static int Fibonacci(int n)
    {
        if (n <= 1)
            return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    static int FindN(double a, double b, double l)
    {
        double length = b - a;
        int N = 0;
        while (Fibonacci(N) < length / l)
            N++;
        return N;
    }

    static double FibonacciSearch(double a, double b, double epsilon)
    {
        int N = FindN(a, b, epsilon);
        int k = 0;

        double y = a + (double)Fibonacci(N - 2) / Fibonacci(N) * (b - a);
        double z = a + (double)Fibonacci(N - 1) / Fibonacci(N) * (b - a);

        double fy = Function(y);
        double fz = Function(z);

        while (k != N - 3)
        {
            if (fy <= fz)
            {
                b = z;
                z = y;
                y = a + (double)Fibonacci(N - k - 3) / Fibonacci(N - k - 1) * (b - a);
                fz = fy;
                fy = Function(y);
            }
            else
            {
                a = y;
                y = z;
                z = a + (double)Fibonacci(N - k - 2) / Fibonacci(N - k - 1) * (b - a);
                fy = fz;
                fz = Function(z);
            }
            k++;

            Console.WriteLine(a.ToString() + "\n" +
                                  b.ToString() + "\n" +
                                  z.ToString() + "\n" +
                                  y.ToString() + "\n" +
                                  fy.ToString() + "\n" +
                                  fz.ToString() + "\n");
        }

        // Final N-th function evaluation
        double yN = (a + b) / 2;
        double zN = yN;

        if (k == N - 3)
        {
            yN = zN = (a + b) / 2;
            zN = yN + epsilon;
        }

        double fyN = Function(yN);
        double fzN = Function(zN);

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
        double a = -1; // Левая граница интервала
        double b = 3; // Правая граница интервала
        double l = 0.3; // Допустимая длина конечного интервала
        double epsilon = 0.3; // Константа различимости

        double min = FibonacciSearch(a, b, l);

        Console.WriteLine($"Минимум функции на интервале [{a}, {b}] с длиной интервала {l} и константой различимости {epsilon} равен {min}");
        Console.WriteLine(Function(min));
    }
}
