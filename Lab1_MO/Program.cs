namespace Lab1_MO
{
    class GoldenSectionSearch
    {
        static double GoldenRatio = (3 - Math.Sqrt(5)) / 2; // Значение золотого сечения

        static double Function(double x)
        {
            // Здесь нужно реализовать вашу функцию

            var v = 2 * x * x * x + 9 * x * x - 21; //2 * Math.Pow(x, 3) + 9 * Math.Pow(x, 2) - 21;

            return v; // Пример: квадратичная функция
        }

        static double GoldenSectionSearchMin(double a, double b, double epsilon)
        {
            double y = a + GoldenRatio * (b - a);
            double z = a + b - y;

            double fy = Function(y);
            double fz = Function(z);

            while (Math.Abs(a - b) > epsilon)
            {
                if (fy <= fz)
                {
                    a = a;
                    b = z;
                    z = y;
                    y = a + b - y;
                    fz = fy;

                    fy = Function(y);
                }
                else
                {
                    a = y;
                    b = b;
                    y = z;
                    z = a + b - z;
                    fy = fz;
                    fz = Function(z);
                }

                Console.WriteLine(a.ToString() + "\n" +
                                  b.ToString() + "\n" +
                                  z.ToString() + "\n" +
                                  y.ToString() + "\n" +
                                  fy.ToString() + "\n" +
                                  fz.ToString() + "\n");


            }

            return (a + b) / 2;
        }

        static void Main(string[] args)
        {
            double a = 0; // Начальная левая граница интервала
            a = Convert.ToDouble(Console.ReadLine());
            
            double b = 1; // Начальная правая граница интервала
            b = Convert.ToDouble(Console.ReadLine());
            
            double epsilon = 0.0001; // Требуемая точность

            double min = GoldenSectionSearchMin(a, b, epsilon);

            Console.WriteLine($"Минимум функции на интервале [{a}, {b}] с точностью {epsilon} равен {min}");

            Console.WriteLine(Function(min));
        }
    }

}
