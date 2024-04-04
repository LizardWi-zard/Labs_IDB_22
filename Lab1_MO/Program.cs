using System;

class BisectionMethod
{
    static double F(double x) =>  3 * x + 6;  // 2 * Math.Pow(x, 3) + 9 * Math.Pow(x, 2) - 21;

    static double Bisection(double a, double b, double e)
    {
        var k = 0;

        var x_c = 0.0;
        var halfLength = 0.0;
        var y_c = 0.0;
        var z_c = 0.0;
        var f_c = 0.0;
        var f_y = 0.0;
        var f_z = 0.0;

        do
        {
            x_c = (a + b) / 2;
            halfLength = (b - a) / 2;
            y_c = a + halfLength / 4;
            z_c = b - halfLength / 4;
            f_c = F(x_c);
            f_y = F(y_c);
            f_z = F(z_c);

            Output(k, a, b, x_c, y_c, z_c, f_c, f_y, f_z);

            if (f_y < f_c)
            {
                b = x_c;
                x_c = y_c;
            }
            else
            {
                if (f_z < f_c)
                {
                    a = x_c;
                    x_c = z_c;
                }
                else
                {
                    a = y_c;
                    b = z_c;
                }
            }

            k++;

        } while ((b - a) > e);

        return (a + b) / 2;
    }

    static void Main(string[] args)
    {
        double a = -1;
        double b = 3; 
        double e = 0.3;
        
        double root = Math.Round(Bisection(a, b, e), 6);

        Console.WriteLine($"Корень функции на интервале [{a}, {b}] с точностью {e} равен {root}");
        Console.WriteLine($"Значение функции в {root}: " + Math.Round(F(root), 6));
    }

    static void Output(int k, double a, double b, double x_c, double y_c, double z_c, double f_c, double f_y, double f_z)
    {
        Console.WriteLine($"\nитерация {k}:");
        Console.WriteLine("a: " + Math.Round(a, 6) + "\n" +
                          "b: " + Math.Round(b, 6) + "\n" +
                          "x_c: " + Math.Round(x_c, 6) + "\n" +
                          "f_c: " + Math.Round(f_c, 6) + "\n" +
                          "y_c: " + Math.Round(y_c, 6) + "\n" +
                          "f_y: " + Math.Round(f_y, 6) + "\n" +
                          "z_c: " + Math.Round(z_c, 6) + "\n" +
                          "f_z: " + Math.Round(f_z, 6) + "\n");
    }
}
