namespace Lab1_MO
{
    class GoldenSectionSearch
    {
        static double GoldenRatio = (3 - Math.Sqrt(5)) / 2;

        static double F(double x) => 2 * Math.Pow(x, 3) + 9 * Math.Pow(x, 2) - 21;

        static double GoldenSearch(double a, double b, double e)
        {
            var k = 0;

            var y = a + GoldenRatio * (b - a);
            var z = a + b - y;

            var f_y = F(y);
            var f_z = F(z);

            Output(k, a, b, y, z, f_y, f_z);

            while (Math.Abs(a - b) > e)
            {
                if (f_y <= f_z)
                {
                    Console.WriteLine("f(y) <= f(z)");
                    a = a;
                    b = z;
                    z = y;
                    y = a + b - y;
                    f_z = f_y;

                    f_y = F(y);
                }
                else
                {
                    Console.WriteLine("f(y) > f(z)");
                    a = y;
                    b = b;
                    y = z;
                    z = a + b - z;
                    f_y = f_z;
                    f_z = F(z);
                }

                k++;

                Output(k, a, b, y, z, f_y, f_z);
            }

            return (a + b) / 2;
        }

        static void Main(string[] args)
        {
            double a = -1;
            double b = 3;

            double e = 0.3; 

            double min = GoldenSearch(a, b, e);

            Console.WriteLine($"Минимум функции на интервале [{a}, {b}] с точностью {e} равен {min}");

            Console.WriteLine("Значение функции по найденному минимуму: " + F(min));
        }

        static void Output(int k, double a, double b, double y, double z, double f_y, double f_z)
        {
            Console.WriteLine($"\nитерация {k}:");
            Console.WriteLine("a: " + Math.Round(a, 6) + "\n" +
                              "b: " + Math.Round(b, 6) + "\n" +
                              "y: " + Math.Round(y, 6) + "\n" +
                              "z: " + Math.Round(z, 6) + "\n" +
                              "f(y): " + Math.Round(f_y, 6) + "\n" +
                              "f(z): " + Math.Round(f_z, 6) + "");
        }
    }                                                 
}