using System;

namespace LW2
{
    internal class Program
    {
        static (double, double) CalculateGradient((double, double) point) => (12 * point.Item1 + 0.4 * point.Item2, 0.4 * point.Item1 + 10 * point.Item2);
        static double FunctionValue((double, double) point) => 6 * point.Item1 * point.Item1 + 0.4 * point.Item1 * point.Item2 + 5 * point.Item2 * point.Item2;
        static double Norm((double, double) point) => (Math.Sqrt(Math.Pow(point.Item1, 2) + Math.Pow(point.Item2, 2)));
      
        static bool IsAlgorithmEnded((double, double) previousPoint, (double, double) currentPoint, (double, double) nextPoint, double epsilon)
        {
            var condition1 = Norm((nextPoint.Item1 - currentPoint.Item1, nextPoint.Item2 - currentPoint.Item2)) < epsilon && Math.Abs(FunctionValue(nextPoint) - FunctionValue(currentPoint)) < epsilon;
            var condition2 = Norm((currentPoint.Item1 - previousPoint.Item1, currentPoint.Item2 - previousPoint.Item2)) < epsilon && Math.Abs(FunctionValue(currentPoint) - FunctionValue(previousPoint)) < epsilon;
            return condition1 && condition2;
        }

        static void PrintIterationInfo(int iteration, (double, double) currentPoint, (double, double) gradient, double functionValue, double norm)
        {
            Console.WriteLine($"Current point: ({Math.Round(currentPoint.Item1, 4)}, {Math.Round(currentPoint.Item2, 4)})\n" +
                              $"Gradient: ({Math.Round(gradient.Item1, 4)}, {Math.Round(gradient.Item2, 4)})\n" +
                              $"Norm of currentGradient in point: {Math.Round(norm, 4)}" +
                              $"Function value: {Math.Round(functionValue, 4)}\n");
        }

        static ((double, double), int) ConstantStepGradientDescent((double, double) startingPoint, double epsilon, double epsilonGradient, double epsilonDifference, int maxIterations)
        {
            var previousPoint = startingPoint;
            var currentPoint = startingPoint;
            var nextPoint = startingPoint;
            double step;;
            int iteration = 0;

            while (true)
            {
                Console.WriteLine($"Iteration {iteration + 1}:");

                var currentGradient = CalculateGradient(currentPoint);
                if (Norm(currentGradient) < epsilonGradient || iteration >= maxIterations)
                {
                    return (currentPoint, iteration);
                }

                Console.WriteLine("Enter step:");
                step = Double.Parse(Console.ReadLine());

                var a = 0;
                while (FunctionValue(nextPoint) - FunctionValue(currentPoint) >= 0)
                {
                    nextPoint.Item1 = currentPoint.Item1 - step * currentGradient.Item1;
                    nextPoint.Item2 = currentPoint.Item2 - step * currentGradient.Item2;
                    step /= 2;

                }

                PrintIterationInfo(iteration, currentPoint, currentGradient, FunctionValue(currentPoint), Norm(currentGradient));

                if (IsAlgorithmEnded(previousPoint, currentPoint, nextPoint, epsilonDifference))
                {
                    return (nextPoint, iteration);
                }
                else
                {
                    iteration++;
                    previousPoint = currentPoint;
                    currentPoint = nextPoint;
                }
            }
        }

        static void Main(string[] args)
        {
            var startingPoint = (0.0, 0.5);
            var epsilon = 0.1;
            var epsilonGradient = 0.15;
            var epsilonDifference = 0.2;
            int maxIterations = 10;
            var result = ConstantStepGradientDescent(startingPoint, epsilon, epsilonGradient, epsilonDifference, maxIterations);
            Console.WriteLine($"x = ({Math.Round(result.Item1.Item1, 4)};{Math.Round(result.Item1.Item2, 4)}), f(x) = {Math.Round(FunctionValue(result.Item1), 4)}, iterations = {result.Item2}");
        }
    }
}