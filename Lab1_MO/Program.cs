using System;

class Program
{
    static void Main(string[] args)
    {
        double E1 = 0.15; // Условие для нормы градиента
        double E2 = 0.2; // Условие для изменения значения функции и точности аргумента
        int M = 10; // Максимальное число итераций

        double a = 6; // Коэффициент a
        double b = 0.4; // Коэффициент b
        double c = 5; // Коэффициент c

        int k = 0; // Счетчик итераций
        double[] x = new double[] { 1.5, 0.5 }; // Начальное значение аргумента

        Console.WriteLine("Итерация 0:");
        PrintVariables(a, b, c, E1, E2, M, k, x);

        while (true)
        {
            double[] gradient = Gradient(x, a, b, c); // Шаг 3: Вычисляем градиент в точке x^k
            Console.WriteLine($"Градиент: ({gradient[0]}, {gradient[1]})");

            if (Norm(gradient) < E1)
            {
                Console.WriteLine("Оптимальное значение найдено.");
                Console.WriteLine($"x* = ({x[0]}, {x[1]})");
                break;
            }

            if (k >= M)
            {
                Console.WriteLine("Оптимальное значение найдено.");
                Console.WriteLine($"x* = ({x[0]}, {x[1]})");
                break;
            }

            double[,] Hessian = HessianMatrix(a, b, c); // Шаг 6: Вычисляем матрицу Гессе в точке x^k
            Console.WriteLine($"Матрица Гессе: [{Hessian[0, 0]}, {Hessian[0, 1]}; {Hessian[1, 0]}, {Hessian[1, 1]}]");

            double[,] inverseHessian = InverseMatrix(Hessian); // Шаг 7: Вычисляем обратную матрицу Гессе
            Console.WriteLine($"Обратная матрица Гессе: [{inverseHessian[0, 0]}, {inverseHessian[0, 1]}; {inverseHessian[1, 0]}, {inverseHessian[1, 1]}]");

            if (IsPositiveDefinite(inverseHessian)) // Шаг 8: Проверяем положительную определенность матрицы
            {
                double[] dk = MultiplyMatrixVector(inverseHessian, gradient); // Шаг 9: Находим направление dk
                Console.WriteLine($"Направление: ({dk[0]}, {dk[1]})");

                double tk = 1; // Шаг 10: Инициализируем шаг t_k

                double[] newX = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newX[i] = x[i] + tk * dk[i]; // Шаг 10: Находим новую точку x^(k+1)
                }
                Console.WriteLine($"Новая точка: ({newX[0]}, {newX[1]})");

                if (Function(newX, a, b, c) < Function(x, a, b, c)) // Шаг 11: Проверяем условие улучшения
                {
                    if (Norm(newX) < E2 || Math.Abs(Function(newX, a, b, c) - Function(x, a, b, c)) < E2) // Проверяем условие на точность
                    {
                        Console.WriteLine("Оптимальное значение найдено.");
                        Console.WriteLine($"x* = ({newX[0]}, {newX[1]})");
                        break;
                    }
                    else
                    {
                        x = newX; // Переходим к новой точке
                        k++;
                        Console.WriteLine($"Итерация {k}:");
                        PrintVariables(a, b, c, E1, E2, M, k, x);
                    }
                }
                else
                {
                    k++;
                    Console.WriteLine($"Итерация {k}:");
                    PrintVariables(a, b, c, E1, E2, M, k, x);
                }
            }
            else
            {
                double[] dk = MultiplyScalarVector(-1, gradient); // Вычисляем d^k = -∇f(x^k)
                Console.WriteLine($"Направление: ({dk[0]}, {dk[1]})");

                double tk = 1; // Инициализируем шаг t_k

                double[] newX = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    newX[i] = x[i] + tk * dk[i]; // Находим новую точку x^(k+1)
                }
                Console.WriteLine($"Новая точка: ({newX[0]}, {newX[1]})");

                if (Function(newX, a, b, c) < Function(x, a, b, c)) // Проверяем условие на улучшение
                {
                    if (Norm(newX) < E2 || Math.Abs(Function(newX, a, b, c) - Function(x, a, b, c)) < E2) // Проверяем условие на точность
                    {
                        Console.WriteLine("Оптимальное значение найдено.");
                        Console.WriteLine($"x* = ({newX[0]}, {newX[1]})");
                        break;
                    }
                    else
                    {
                        x = newX; // Переходим к новой точке
                        k++;
                        Console.WriteLine($"Итерация {k}:");
                        PrintVariables(a, b, c, E1, E2, M, k, x);
                    }
                }
                else
                {
                    k++;
                    Console.WriteLine($"Итерация {k}:");
                    PrintVariables(a, b, c, E1, E2, M, k, x);
                }
            }
        }
    }

    static void PrintVariables(double a, double b, double c, double E1, double E2, int M, int k, double[] x)
    {
        Console.WriteLine($"\na = {a}, b = {b}, c = {c}");
        Console.WriteLine($"E1 = {E1}, E2 = {E2}, M = {M}");
        Console.WriteLine($"k = {k}");
        Console.WriteLine($"x = ({x[0]}, {x[1]})");
    }

    static double Function(double[] x, double a, double b, double c)
    {
        return a * Math.Pow(x[0], 2) + b * x[0] * x[1] + c * Math.Pow(x[1], 2); // Функция ax1^2 + bx1x2 + cx2^2
    }


    static double[] Gradient(double[] x, double a, double b, double c)
    {
        double[] gradient = new double[2];
        gradient[0] = a * 2 * x[0] + b * x[1];
        gradient[1] = b * x[0] + 2 * c * x[1];
        return gradient;
    }

    static double[,] HessianMatrix(double a, double b, double c)
    {
        return new double[,] { { a * 2, b }, { b, c * 2 } };
    }

    static double[,] InverseMatrix(double[,] matrix)
    {
        double det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        double[,] inverse = new double[,] { { matrix[1, 1] / det, -matrix[0, 1] / det }, { -matrix[1, 0] / det, matrix[0, 0] / det } };
        return inverse;
    }

    static bool IsPositiveDefinite(double[,] matrix)
    {
        return matrix[0, 0] > 0 && matrix[1, 1] > 0 && (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]) > 0;
    }

    static double Norm(double[] vector)
    {
        return Math.Sqrt(Math.Pow(vector[0], 2) + Math.Pow(vector[1], 2));
    }

    static double[] MultiplyMatrixVector(double[,] matrix, double[] vector)
    {
        double[] result = new double[matrix.GetLength(0)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            double sum = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                sum += matrix[i, j] * vector[j];
            }
            result[i] = sum;
        }
        return result;
    }

    static double[] MultiplyScalarVector(double scalar, double[] vector)
    {
        double[] result = new double[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            result[i] = scalar * vector[i];
        }
        return result;
    }
}
