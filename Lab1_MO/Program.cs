namespace Lab1_MO
{
    class GoldenSectionSearch
    {
        static double GoldenRatio = (3 - Math.Sqrt(5)) / 2;

        static double F(double x) => 2 * Math.Pow(x, 3) + 9 * Math.Pow(x, 2) - 21;

        static double GoldenSectionSearchMin(double a, double b, double e)
        {
            var k = 0;

            var y = a + GoldenRatio * (b - a);
            var z = a + b - y;

            var fy = F(y);
            var fz = F(z);

            Output(k, a, b, y, z, fy, fz);

            while (Math.Abs(a - b) > e)
            {
                if (fy <= fz)
                {
                    a = a;
                    b = z;
                    z = y;
                    y = a + b - y;
                    fz = fy;

                    fy = F(y);
                }
                else
                {
                    a = y;
                    b = b;
                    y = z;
                    z = a + b - z;
                    fy = fz;
                    fz = F(z);
                }

                k++;

                Output(k, a, b, y, z, fy, fz);
            }

            return (a + b) / 2;
        }

        static void Main(string[] args)
        {
            double a = -1; // Начальная левая граница интервала
            double b = 3; // Начальная правая граница интервала

            double e = 0.3; 

            double min = GoldenSectionSearchMin(a, b, e);

            Console.WriteLine($"Минимум функции на интервале [{a}, {b}] с точностью {e} равен {min}");

            Console.WriteLine("Значение функции по найденному минимуму: " + F(min));
        }

        static void Output(int k, double a, double b, double y, double z, double fy, double fz)
        {
            Console.WriteLine($"\niteration {k}:");
            Console.WriteLine("a:" + Math.Round(a, 6) + "\n" +
                              "b:" + Math.Round(b, 6) + "\n" +
                              "y:" + Math.Round(y, 6) + "\n" +
                              "z:" + Math.Round(z, 6) + "\n" +
                              "fy:" + Math.Round(fy, 6) + "\n" +
                              "fz:" + Math.Round(fz, 6) + "\n");
        }
    }                                                 
}