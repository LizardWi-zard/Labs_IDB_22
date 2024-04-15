using System;

class GradientDescent
{
    // Определяем функцию, для которой ищем минимум
    static double Function(double[] x)
    {
        // Функция f(x) = 6 * x1^2 + 0.4 * x1 * x2 + 5 * x2^2
        return 6 * x[0] * x[0] + 0.4 * x[0] * x[1] + 5 * x[1] * x[1];
    }

    // Вычисление градиента функции в точке x
    static double[] Gradient(double[] x)
    {
        // Градиент функции f(x) = 6 * x1^2 + 0.4 * x1 * x2 + 5 * x2^2
        double df_dx1 = 12 * x[0] + 0.4 * x[1];
        double df_dx2 = 0.4 * x[0] + 10 * x[1];
        return new double[] { df_dx1, df_dx2 };
    }

    // Метод для выполнения градиентного спуска
    static double[] GradientDescentAlgorithm(double[] x, double E, double E1, double E2, int M)
    {
        double[] gradient = new double[x.Length];

        for (int k = 0; k < M; k++)
        {
            gradient = Gradient(x); // Шаг 3

            // Вывод текущей итерации
            Console.WriteLine($"Итерация {k + 1}:");
            Console.WriteLine($"Текущие значения переменных: x = [{x[0]}; {x[1]}]");
            Console.WriteLine($"Градиент: ∇f(x) = [{gradient[0]}; {gradient[1]}]");

            // Шаг 4
            if (Math.Sqrt(gradient[0] * gradient[0] + gradient[1] * gradient[1]) < E)
            {
                Console.WriteLine("Критерий выполнен");
                break;
            }

            // Шаг 5
            if (k >= M - 1)
            {
                Console.WriteLine("Достигнуто предельное число итераций");
                break;
            }

            // Шаг 6 (можно выбрать различные стратегии выбора величины шага t_k)
            double t_k = 0.1;

            // Шаг 7
            double[] newX = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                newX[i] = Math.Round(x[i] - t_k * gradient[i], 4);
            }

            // Вывод измененных переменных
            Console.WriteLine($"Новые значения переменных: x = [{newX[0]}, {newX[1]}]");

            // Шаг 8
            if (Function(newX) - Function(x) < 0)
            {
                // Шаг 9
                if (Math.Sqrt(newX[0] * newX[0] + newX[1] * newX[1]) < E2 &&
                    Math.Abs(Function(newX) - Function(x)) < E2)
                {
                    Console.WriteLine("Расчет окончен");
                    return newX;
                }
                else
                {
                    x = newX;
                    continue;
                }
            }
            else
            {
                t_k /= 2;
                continue;
            }
        }

        return x;
    }

    static void Main()
    {
        double E = 0.3; // Погрешность
        double E1 = 0.15; // Погрешность 1
        double E2 = 0.2; // Погрешность 2
        int M = 10; // Предельное число итераций

        double[] x = new double[] { 0, 0.5 }; // Начальная точка

        double[] result = GradientDescentAlgorithm(x, E, E1, E2, M);
        Console.WriteLine($"Минимум функции: f({result[0]}, {result[1]}) = {Function(result)}");
    }
}