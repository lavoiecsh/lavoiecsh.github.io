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
            var solver = GetSolver(day, args.Skip(2));
            var watch = Stopwatch.StartNew();
            var solution = problem == 1 ? solver.SolveFirstPart() : solver.SolveSecondPart();
            watch.Stop();
            Console.WriteLine($"Solution to problem {day}-{problem} ({solver.ProblemName}): {solution} (found in {watch.ElapsedMilliseconds}ms)");
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
                    return new Day03Solver(new ClaimFileReaderDataProvider(args.First()));
                case 4:
                    return new Day04Solver(new GuardFileReaderDataProvider(args.First()));
                case 5:
                    return new Day05Solver(new SimpleFileReaderDataProvider(args.First()));
                default:
                    return null;
            }
        }
    }
}