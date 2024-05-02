namespace LW3
{
    internal class Program
    {
        public static double a = 0;
        public static double b = 0;
        public static double c = 0;

        static (double, double) GetGradient((double, double) x) => (2 * a * x.Item1 + b * x.Item2, b * x.Item1 + 2 * c * x.Item2);
        static double F((double, double) x) => a * Math.Pow(x.Item1, 2) + b * x.Item1 * x.Item2 + c * Math.Pow(x.Item2, 2);
        static double Norm((double, double) x) => Math.Sqrt(Math.Pow(x.Item1, 2) + Math.Pow(x.Item2, 2));
        static double GetDet(double[,] matrix) => matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
        
        static bool StopSearch((double, double) previousP, (double, double) currentP, (double, double) nextP, double e2)
        {
            var normOfNextPoint = Norm((nextP.Item1 - currentP.Item1, nextP.Item2 - currentP.Item2));
            var normOfPrevPoint = Norm((currentP.Item1 - previousP.Item1, currentP.Item2 - previousP.Item2));
            var absNext = Math.Abs(F(nextP) - F(currentP));
            var absPrev = Math.Abs(F(currentP) - F(previousP));

            Console.WriteLine($"-------\nNorm of (next x - x) = {Math.Round(normOfNextPoint, 4)}\n" +
                              $"Norm of (x - previous x) = {Math.Round(normOfPrevPoint, 4)}\n" +
                              $"|f(next x) - f(x)| = |{Math.Round(absNext, 4)}|\n" +
                              $"|f(x) - f(previous x)| = |{Math.Round(absPrev, 4)}|\n-------");

            if (!(normOfNextPoint < e2))
            {
                Console.WriteLine("||(next x - x)|| >= e2\n");
                return false;
            }
            if (!(normOfPrevPoint < e2))
            {
                Console.WriteLine("||(x - previous x)|| >= e2\n");
                return false;
            }
            if (!(absNext < e2))
            {
                Console.WriteLine("|f(next x) - f(x)| >= e2\n");
                return false;
            }
            if (!(absPrev < e2))
            {
                Console.WriteLine("|f(x) - f(previous x)| >= e2\n");
                return false;
            }

            return true;
        }

        static double[,] GetGessianMatrix()
        {
            double[,] result = { { 2 * a, b }, { b, 2 * c } };
            Console.WriteLine($"Gessian matrix:\n|  {Math.Round(result[0, 0], 4)}; {Math.Round(result[0, 1], 4)} |\n" +
                                               $"| {Math.Round(result[1, 0], 4)};  {Math.Round(result[1, 1], 4)} |\n-------");
            return result;
        }

        static double[,] GetReversedGessianMatrix(double detGessian)
        {
            detGessian = Math.Abs(detGessian);

            double[,] result = { { 2 * c / detGessian, -b / detGessian }, 
                                    { -b / detGessian, 2 * a / detGessian } };

            Console.WriteLine($"Reversed Gessian matrix:\n|  {Math.Round(result[0, 0], 4)}; {Math.Round(result[0, 1], 4)} |\n" +
                                                        $"| {Math.Round(result[1, 0], 4)};  {Math.Round(result[1, 1], 4)} |\n-------");
            return result;
        }


        static ((double, double), int) Method((double, double) x, double e1, double e2, int M)
        {
            var previousP = x;
            var currentP = x;
            var nextP = x;
            double step = 1;
            int k = 0;

            Console.WriteLine($"f(x) = {a}x1^2 + {b}x1x2 + {c}x2^2, x0 = ({currentP.Item1};{currentP.Item2}), epsilon1 = {e1}, epsilon2 = {e2}, M = {M}\n" +
                              $"Gradient(x) = ({2 * a}x1 + {b}x2;{b}x1 + {2 * c}x2)\nH(x):\n|{2 * a} {b}|\n|{b} {2 * c} |");
            
            Console.WriteLine($"Function at the start: {Math.Round(F(currentP), 4)}\n");

            while (true)
            {
                Console.WriteLine($"\nIteration {k}:");

                var curGrad = GetGradient(currentP);

                Console.WriteLine($"current x: ({Math.Round(currentP.Item1, 4)}; {Math.Round(currentP.Item2, 4)}) \n" +
                                  $"F: {Math.Round(F(currentP), 4)}\nGradient of Function: ({Math.Round(curGrad.Item1, 4)}; {Math.Round(curGrad.Item2, 4)})");

                if (Norm(curGrad) <= e1)
                {
                    return (currentP, k);
                }
                else
                {
                    Console.WriteLine($"Norm of gradient >= epsilon1\n{Math.Round(Norm(curGrad), 4)} >= {e1}\n-------");
                }

                if (k >= M)
                {
                    return (currentP, k);
                }
                else
                {
                    Console.WriteLine($"k < M\n{k} < {M}\n-------");
                }

                var reversedGessian = GetReversedGessianMatrix(GetDet(GetGessianMatrix()));
                var d = (0.0, 0.0);
                bool flag = false;

                Console.WriteLine($"det(H^-1(xk)) = {Math.Round(GetDet(reversedGessian), 4)}");
                
                if (reversedGessian[0, 0] > 0 && GetDet(reversedGessian) > 0)
                {
                    d = (-(reversedGessian[0, 0] * curGrad.Item1 + reversedGessian[0, 1] * curGrad.Item2), 
                         -(reversedGessian[1, 0] * curGrad.Item1 + reversedGessian[1, 1] * curGrad.Item2));

                    Console.WriteLine($"det(H^-1(xk)) > 0 \n-------");
                }
                else
                {
                    d = (-curGrad.Item1, 
                         -curGrad.Item2);
                    flag = true;

                    Console.WriteLine($"det(H^-1(xk)) <= 0 \n-------");
                }

                Console.WriteLine($"d^k: ({Math.Round(d.Item1, 4)}; {Math.Round(d.Item2, 4)})\n-------");

                if (flag == false)
                {
                    step = 1;
                  
                    nextP = (currentP.Item1 + step * d.Item1,
                             currentP.Item2 + step * d.Item2);
                }
                else
                {
                    nextP = (currentP.Item1 + step * d.Item1,
                             currentP.Item2 + step * d.Item2);

                    while(F(nextP) >= F(currentP))
                    {
                        step /= 2;

                        nextP = (currentP.Item1 + step * d.Item1,
                                 currentP.Item2 + step * d.Item2);
                    }
                }

                Console.WriteLine($"nextX: ({Math.Round(nextP.Item1, 4)}; {Math.Round(nextP.Item1, 4)})");

                if (StopSearch(previousP, currentP, nextP, e2))
                {
                    return (nextP, k);
                }
                else
                {
                    k++;
                    previousP = currentP;
                    currentP = nextP;
                }
            }
        }

        static void Main()
        {
            /* Console.WriteLine("Enter x1 and x2 for starting point\nx0 = (x1; x2);");
             Console.WriteLine("x1:");
             var x0_1 = Double.Parse(Console.ReadLine());
             Console.WriteLine("x2:");
             var x0_2 = Double.Parse(Console.ReadLine());
             var x0 = (x0_1, x0_2);
             Console.WriteLine("Enter first epsilon:");
             var e1 = Double.Parse(Console.ReadLine());
             Console.WriteLine("Enter second epsilon:");
             var e2 = Double.Parse(Console.ReadLine());
             int M = 10;*/

            var x0 = (1.5, 0.5);
            var e1 = 0.15;
            var e2 = 0.2;
            int M = 10;

            Console.Clear();

            Console.WriteLine("Enter a, b, c for equation\nax1^2 + bx1x2 + cx2^2");
            a = Double.Parse(Console.ReadLine());
            b = Double.Parse(Console.ReadLine());
            c = Double.Parse(Console.ReadLine());

            var result = Method(x0, e1, e2, M);
            Console.WriteLine($"Result:\nx = ({Math.Round(result.Item1.Item1, 4)}; {Math.Round(result.Item1.Item2, 4)})\nf(x) = {Math.Round(F(result.Item1), 4)}\nk = {result.Item2}");
        }
    }
}