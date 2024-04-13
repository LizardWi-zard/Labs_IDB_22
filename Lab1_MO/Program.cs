using System;

class BisectionMethod
{
    static double F(double x) =>  2 * Math.Pow(x, 3) + 9 * Math.Pow(x, 2) - 21;

    static double Bisection(double a, double b, double e, out int k)
    {
        k = 0;
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
            y_c = a + halfLength / 2;
            z_c = b - halfLength / 2;
            f_c = F(x_c);
            f_y = F(y_c);
            f_z = F(z_c);

            Output(k, a, b, x_c, y_c, z_c, f_c, f_y, f_z);

            if (f_y < f_c)
            {
                Console.WriteLine("f(y) < f(c)");

                b = x_c;
                x_c = y_c;
            }
            else
            {

                Console.WriteLine("f(y) > f(c)");

                if (f_z < f_c)
                {
                    Console.WriteLine("f(z) < f(c)");
                    a = x_c;
                    x_c = z_c;
                }
                else
                {
                    Console.WriteLine("f(z) > f(c)");
                    a = y_c;
                    b = z_c;
                }
            }

            k++;

        } while (Math.Abs(b - a) > e);

        return (a + b) / 2;
    }

    static void Main(string[] args)
    {
        double a = -1;
        double b = 3; 
        double e = 0.3;

        int k = 0;
        
        double root = Math.Round(Bisection(a, b, e, out k), 6);

        Console.WriteLine($"Функции на интервале [{a}, {b}] с точностью {e} равна {Math.Round(root, 4)}");
        Console.WriteLine($"количество итераций: {k}");
        Console.WriteLine($"Значение функции в {Math.Round(root, 4)}: " + Math.Round(F(root), 4));
    }

    static void Output(int k, double a, double b, double x_c, double y_c, double z_c, double f_c, double f_y, double f_z)
    {
        Console.WriteLine($"\nитерация {k}:");
        Console.WriteLine("a: " + Math.Round(a, 4) + "\n" +
                          "b: " + Math.Round(b, 4) + "\n" +
                          "x_c: " + Math.Round(x_c, 4) + "\n" +
                          "f_c: " + Math.Round(f_c, 4) + "\n" +
                          "y_c: " + Math.Round(y_c, 4) + "\n" +
                          "f_y: " + Math.Round(f_y, 4) + "\n" +
                          "z_c: " + Math.Round(z_c, 4) + "\n" +
                          "f_z: " + Math.Round(f_z, 4) + "");
    }
}
