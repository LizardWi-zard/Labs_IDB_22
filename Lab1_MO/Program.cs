using System;

class GradientDescent
{
    // Функция, чьи минимум мы ищем
    static double Function(double[] x)
    {
        //   7: f(x) = 6 * x1^2 + 0.4 * x1 * x2 + 5 * x2^2
        //  11: f(x) = 5 * x1^2 + 0.6 * x1 * x2 + 2 * x2^2
        return 5 * x[0] * x[0] + 0.6 * x[0] * x[1] + 2 * x[1] * x[1];//6 * x[0] * x[0] + 0.4 * x[0] * x[1] + 5 * x[1] * x[1];
    }

    // Градиент функции
    static double[] Gradient(double[] x)
    {
        // Производные по каждой переменной
        double df_dx1 = Math.Round(10 * x[0] + 0.6 * x[1], 4);//12 * x[0] + 0.4 * x[1], 4);
        double df_dx2 = Math.Round(0.6 * x[0] + 4 * x[1], 4); //0.4 * x[0] + 10 * x[1], 4);
        // 10 * x[0] + 0.6 * x[1]
        // 0.6 * x[0] + 4 * x[1]
        return new double[] { df_dx1, df_dx2 };
    }

    // Метод градиентного спуска
    static double[] GradientDescentMethod(double[] x, double E1, double E2, int M)
    {
        int k = 0;
        while (true)
        {
            // Шаг 3: Вычислить градиент функции
            double[] gradient = Gradient(x);

            // Шаг 4: Проверить выполнение критерия окончания
            if (Norm(gradient) < E1)
            {
                Console.WriteLine("Критерий окончания выполнен.");
                break;
            }

            // Шаг 5: Проверить выполнение предельного числа итераций
            if (k >= M)
            {
                Console.WriteLine("Достигнуто предельное число итераций.");
                break;
            }

            // Шаг 6: Вычислить величину шага
            double step = ComputeStep(x, gradient);

            // Шаг 7: Вычислить новое значение x
            double[] newX = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                newX[i] = Math.Round(x[i] - step * gradient[i], 4);
            }

            // Вывод текущей итерации
            Console.WriteLine($"Итерация {k + 1}: x = ({Math.Round(x[0], 4)}, {Math.Round(x[1],4)}), f(x) = {Math.Round(Function(x), 4)}");

            // Шаг 8: Проверить выполнение условия остановки
            if (Norm(newX) - Norm(x) < E2 && Math.Abs(Function(newX) - Function(x)) < E2)
            {
                Console.WriteLine("Условие остановки выполнено.");
                break;
            }

            x = newX;
            k++;
        }

        return x;
    }

    // Шаг 6: Вычислить величину шага
    static double ComputeStep(double[] x, double[] gradient)
    {
        // В данном примере используем постоянный шаг
        return 0.01;
    }

    static double Norm(double[] vector)
    {
        double sum = 0;
        for (int i = 0; i < vector.Length; i++)
        {
            sum += vector[i] * vector[i];
        }
        return Math.Sqrt(sum);
    }

    static void Main(string[] args)
    {
        double[] x = { 0, 0.5 }; // Начальное значение x
        double E1 = 0.15; // Погрешность для критерия окончания
        double E2 = 0.2; // Погрешность для условия остановки
        int M = 10; // Предельное число итераций

        // Вызов метода градиентного спуска с выводом
        double[] result = GradientDescentMethod(x, E1, E2, M);

        Console.WriteLine("Минимум функции: " + Function(result));
        Console.WriteLine("При x = (" + result[0] + ", " + result[1] + ")");
    }
}
