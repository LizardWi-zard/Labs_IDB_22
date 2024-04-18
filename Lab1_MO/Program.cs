﻿using System;

namespace LW2
{
    internal class Program
    {
        static (double, double) CalculateGradient((double, double) point) => (12 * point.Item1 + 0.4 * point.Item2, 0.4 * point.Item1 + 10 * point.Item2);
        static double FunctionValue((double, double) point) => 6 * point.Item1 * point.Item1 + 0.4 * point.Item1 * point.Item2 + 5 * point.Item2 * point.Item2;
        static double Norm((double, double) point) => (Math.Sqrt(Math.Pow(point.Item1, 2) + Math.Pow(point.Item2, 2)));
      
        static bool IsAlgorithmEnded((double, double) previousPoint, (double, double) currentPoint, (double, double) nextPoint, double epsilon)
        {
            var condition1 = Norm((nextPoint.Item1 - currentPoint.Item1, nextPoint.Item2 - currentPoint.Item2)) < epsilon;
            var condition2 = Norm((currentPoint.Item1 - previousPoint.Item1, currentPoint.Item2 - previousPoint.Item2)) < epsilon;
            var condition3 = Math.Abs(FunctionValue(nextPoint) - FunctionValue(currentPoint)) < epsilon;
            var condition4 = Math.Abs(FunctionValue(currentPoint) - FunctionValue(previousPoint)) < epsilon;

            if (!condition1)
            {
                Console.WriteLine("Norm of (next x - x) >= epsilon2\n");
                return false;
            }
            if (!condition2)
            {
                Console.WriteLine("f(next x) - f(x) >= epsilon2\n");
                return false;
            }
            if (!condition3)
            {
                Console.WriteLine("Norm of (x - previous x) >= epsilon2\n");
                return false;
            }
            if (!condition4)
            {
                Console.WriteLine("f(x) - f(previous x) >= epsilon2\n");
                return false;
            }
            return true;
        }

        static void PrintIterationInfo(int iteration, (double, double) currentPoint, (double, double) gradient, double functionValue, double norm)
        {
            Console.WriteLine($"Current point: ({Math.Round(currentPoint.Item1, 4)}, {Math.Round(currentPoint.Item2, 4)})\n" +
                              $"Gradient: ({Math.Round(gradient.Item1, 4)}, {Math.Round(gradient.Item2, 4)})\n" +
                              $"Norm of gradient in point: {Math.Round(norm, 4)}\n" +
                              $"Function value: {Math.Round(functionValue, 4)}");
        }

        static ((double, double), int) ConstantStepGradientDescent((double, double) startingPoint, double epsilon, double epsilonGradient, double epsilonDifference, int maxIterations)
        {
            var previousPoint = startingPoint;
            var currentPoint = startingPoint;
            var nextPoint = startingPoint;
            double step;
            int iteration = 0;

            while (true)
            {
                Console.WriteLine($"k {iteration }:");

                var currentGradient = CalculateGradient(currentPoint);

                var normIsOk = Norm(currentGradient) < epsilonGradient;
                var iterationCap = iteration >= maxIterations;

                if (normIsOk)
                {
                    return (currentPoint, iteration);
                }
                else
                {
                    Console.WriteLine("norm of gradient >= epsilon");
                }

                if (iterationCap)
                {
                    return (currentPoint, iteration);
                }
                else
                {
                    Console.WriteLine("K < M");
                }

                Console.WriteLine("Enter step:");
                step = Double.Parse(Console.ReadLine());

                var a = 0;
                while (FunctionValue(nextPoint) - FunctionValue(currentPoint) >= 0)
                {
                    nextPoint.Item1 = currentPoint.Item1 - step * currentGradient.Item1;
                    nextPoint.Item2 = currentPoint.Item2 - step * currentGradient.Item2;
                    step /= 2;
                    Console.WriteLine($"Step: {step}");
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

                Console.WriteLine($"Next point: ({Math.Round(nextPoint.Item1, 4)}, {Math.Round(nextPoint.Item2, 4)})\n");
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

            Console.WriteLine($"\nx = ({Math.Round(result.Item1.Item1, 4)};{Math.Round(result.Item1.Item2, 4)}), f(x) = {Math.Round(FunctionValue(result.Item1), 4)}, iterations = {result.Item2}");
        }
    }
}