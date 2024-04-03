using System;

class BisectionMethod
{
    static double Function(double x)
    {
        var v = 2 * x * x * x + 9 * x * x - 21; //2 * Math.Pow(x, 3) + 9 * Math.Pow(x, 2) - 21;

        return v; // Пример: квадратичная функция
    }

    static double Bisection(double a, double b, double epsilon)
    {
        double fa = Function(a);
        double fb = Function(b);

        if (fa * fb >= 0)
        {
            throw new ArgumentException("Функция должна иметь разные знаки на концах интервала [a, b]");
        }

        while ((b - a) > epsilon)
        {
            double xc = (a + b) / 2; // Средняя точка интервала
            double halfLength = (b - a) / 2; // Длина половины интервала

            double yc = a + halfLength / 4; // Точка y
            double zc = b - halfLength / 4; // Точка z

            double fc = Function(xc);
            double fy = Function(yc);
            double fz = Function(zc);

            if (fy < fc)
            {
                b = xc;
                xc = yc;
            }
            else
            {
                if (fz < fc)
                {
                    a = xc;
                    xc = zc;
                }
                else
                {
                    a = yc;
                    b = zc;
                }
            }
        }

        return (a + b) / 2;
    }

    static void Main(string[] args)
    {
        double a = -1; // Левая граница интервала
        double b = 3; // Правая граница интервала
        double epsilon = 0.3; // Требуемая точность

        try
        {
            double root = Bisection(a, b, epsilon);
            Console.WriteLine($"Корень функции на интервале [{a}, {b}] с точностью {epsilon} равен {root}");

            Console.WriteLine(Function(root));
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
    }
}
