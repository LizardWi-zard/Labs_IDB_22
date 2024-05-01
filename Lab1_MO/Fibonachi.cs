namespace LW3
{
    internal class Program
    {
        public static double coef1 { get; set; }
        public static double coef2 { get; set; }
        public static double coef3 { get; set; }

        public class Point
        {
            public double x1;
            public double x2;

            public Point(double x1, double x2)
            {
                this.x1 = x1;
                this.x2 = x2;
            }
        }

        static Point GradFn(Point x)
        {
            var x1 = x.x1;
            var x2 = x.x2;
            var result = new Point(coef1 * x1 + coef2 * x2, coef2 * x1 + coef3 * x2);
            return result;
        }

        static double Function(Point x)
        {
            var x1 = x.x1;
            var x2 = x.x2;
            var result = coef1 * Math.Pow(x1, 2) + coef2 * x1 * x2 + coef3 * Math.Pow(x2, 2);
            return result;
        }

        static double Norm(Point x)
        {
            var x1 = x.x1;
            var x2 = x.x2;
            var result = Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(x2, 2));
            return result;
        }

        static bool IsAlgorithmEnded(Point prevX, Point curX, Point nextX, double epsilon2)
        {
            var condition1 = Norm(new Point(nextX.x1 - curX.x1, nextX.x2 - curX.x2)) < epsilon2;
            if (!condition1)
            {
                Console.WriteLine("Norm of (next x - x) >= epsilon2\n");
                return false;
            }
            var condition2 = Math.Abs(Function(nextX) - Function(curX)) < epsilon2;
            if (!condition2)
            {
                Console.WriteLine("f(next x) - f(x) >= epsilon2\n");
                return false;
            }
            var condition3 = Norm(new Point(curX.x1 - prevX.x1, curX.x2 - prevX.x2)) < epsilon2;
            if (!condition3)
            {
                Console.WriteLine("Norm of (x - previous x) >= epsilon2\n");
                return false;
            }
            var condition4 = Math.Abs(Function(curX) - Function(prevX)) < epsilon2;
            if (!condition4)
            {
                Console.WriteLine("f(x) - f(previous x) >= epsilon2\n");
                return false;
            }
            return true;
        }

        static double[,] Gessian(Point x)
        {
            double[,] result = { { 2 * coef1, coef2 }, { coef2, 2 * coef3 } };
            return result;
        }

        static double[,] ReversedGessian(Point x, double detGessian)
        {
            detGessian = Math.Abs(detGessian);
            double[,] result = { { 2 * coef3 / detGessian, -coef2 / detGessian }, { -coef2 / detGessian, 2 * coef1 / detGessian } };
            return result;
        }

        static double detCalc(double[,] matrix)
        {
            var result = matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
            return result;
        }

        static (Point, int) NewtonMethod(Point x, double epsilon1, double epsilon2, int M)
        {
            var prevX = new Point(x.x1, x.x2);
            var curX = new Point(x.x1, x.x2);
            var nextX = new Point(x.x1, x.x2);
            double step = 0;
            int k = 0;

            while (true)
            {
                var curGrad = GradFn(curX);
                if (Norm(curGrad) <= epsilon1)
                {
                    return (curX, k);
                }
                else
                {
                    Console.WriteLine("Norm of gradient >= epsilon1");
                }
                if (k >= M)
                {
                    return (curX, k);
                }
                else
                {
                    Console.WriteLine("k<M");
                }

                var gessian = Gessian(curX);
                var detGessian = detCalc(gessian);
                var reversedGessian = ReversedGessian(x, detGessian);
                Point d = new Point(0, 0);
                bool flag = false;
                if (reversedGessian[0, 0] > 0 && detCalc(reversedGessian) > 0)
                {
                    d.x1 = -(reversedGessian[0, 0] * curGrad.x1 + reversedGessian[0, 1] * curGrad.x2);
                    d.x2 = -(reversedGessian[1, 0] * curGrad.x1 + reversedGessian[1, 1] * curGrad.x2);
                }
                else
                {
                    d.x1 = -curGrad.x1;
                    d.x2 = -curGrad.x2;
                    flag = true;
                }
                if (flag == false)
                {
                    step = 1;
                }
                else
                {
                    //както искать t
                }
                nextX.x1 = curX.x1 + step * d.x1;
                nextX.x2 = curX.x2 + step * d.x2;

                if (IsAlgorithmEnded(prevX, curX, nextX, epsilon2))
                {
                    return (nextX, k);
                }
                else
                {
                    k++;
                    prevX.x1 = curX.x1;
                    prevX.x2 = curX.x2;
                    curX.x1 = nextX.x1;
                    curX.x2 = nextX.x2;
                }
            }
        }

        static void Main()
        {
            var x0 = new Point(1.5, 0.5);
            var epsilon1 = 0.15;
            var epsilon2 = 0.2;
            int M = 10;

            Console.WriteLine("Enter a, b, c for equation\nax1^2 + bx1x2 + cx2^2");
            coef1 = Double.Parse(Console.ReadLine());
            coef2 = Double.Parse(Console.ReadLine());
            coef3 = Double.Parse(Console.ReadLine());

            var result = NewtonMethod(x0, epsilon1, epsilon2, M);
            Console.WriteLine($"x = ({Math.Round(result.Item1.x1, 4)};{Math.Round(result.Item1.x2, 4)}), f(x) = {Math.Round(Function(result.Item1), 4)}, k = {result.Item2}");
        }
    }
}