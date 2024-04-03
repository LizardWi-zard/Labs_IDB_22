using System;

class BisectionMethod
{
    static double F(double x) =>  2 * Math.Pow(x, 3) + 9 * Math.Pow(x, 2) - 21;

    static double Bisection(double a, double b, double e)
    {
        var k = 0;
       
        while ((b - a) > e)
        {
            var xc = (a + b) / 2; 
            var halfLength = (b - a) / 2;
            var yc = a + halfLength / 4; 
            var zc = b - halfLength / 4; 
            var fc = F(xc);
            var fy = F(yc);
            var fz = F(zc);

            Output(k, a, b, xc, halfLength, yc, zc, fc, fy, fz);

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

            k++;

            Output(k, a, b, xc, halfLength, yc, zc, fc,fy,fz);
        }

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

    static void Output(int k, double a, double b, double xc, double halfLength, double yc, double zc, double fc, double fy, double fz)
    {
        Console.WriteLine($"\niteration {k}:");
        Console.WriteLine("a: " + Math.Round(a, 6) + "\n" +
                          "b: " + Math.Round(b, 6) + "\n" +
                         "xc: " + Math.Round(xc, 6) + "\n" +
                          "halfLength: " + Math.Round(halfLength, 6) + "\n" +
                          "yc: " + Math.Round(yc, 6) + "\n" +
                          "zc: " + Math.Round(zc, 6) + "\n" +
                          "fc: " + Math.Round(fc, 6) + "\n" +
                          "fy: " + Math.Round(fy, 6) + "\n" +
                          "fz: " + Math.Round(fz, 6) + "\n");
    }
}
