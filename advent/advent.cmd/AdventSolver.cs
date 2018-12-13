using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using advent.solvers;
using advent.util;

namespace advent.cmd
{
    internal static class AdventSolver
    {
        private static void Main(string[] args)
        {
            var day = int.Parse(args[0]);
            var problem = int.Parse(args[1]);
            var solver = GetSolver(day, args.Skip(2).ToList());
            var watch = Stopwatch.StartNew();
            var solution = problem == 1 ? solver.SolveFirstPart() : solver.SolveSecondPart();
            watch.Stop();
            Console.WriteLine($"Solution to problem {day}-{problem} ({solver.ProblemName}), found in {watch.ElapsedMilliseconds} ms: {Environment.NewLine}{solution}");
        }

        private static Solver GetSolver(int problem, IEnumerable<string> args)
        {
            switch (problem)
            {
                case 1:
                    return new Day01Solver(new SimpleFileReaderDataProvider(args.First()));
                case 2:
                    return new Day02Solver(new SimpleFileReaderDataProvider(args.First()));
                case 3:
                    return new Day03Solver(new Day03ClaimFileReaderDataProvider(args.First()));
                case 4:
                    return new Day04Solver(new Day04GuardFileReaderDataProvider(args.First()));
                case 5:
                    return new Day05Solver(new SimpleFileReaderDataProvider(args.First()));
                case 6:
                    return new Day06Solver(new Day06LocationFileReaderDataProvider(args.First()));
                case 7:
                    return new Day07Solver(new Day07StepFileReaderDataProvider(args.First()));
                case 8:
                    return new Day08Solver(new Day08NodeFileReaderDataProvider(args.First()));
                case 9:
                    return new Day09Solver(new Day09MarbleGameFileReaderDataProvider(args.First()));
                case 10:
                    return new Day10Solver(new Day10LightFileReaderDataProvider(args.First()));
                case 11:
                    return new Day11Solver(int.Parse(args.First()));
                default:
                    return null;
            }
        }
    }
}