using System;
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
            var argsTail = args.Skip(2);
            var solver = GetSolver(day);
            var solution = problem == 1 ? solver.Solve1(argsTail) : solver.Solve2(argsTail);
            Console.WriteLine($"Solution to problem {day}-{problem}: {solution}");
        }

        private static ISolver GetSolver(int problem)
        {
            switch (problem)
            {
                case 1:
                    return new ChronalCalibrationSolver(new FileReader());
                case 2:
                    return new InventoryManagementSystemSolver(new FileReader());
                default:
                    return null;
            }
        }
    }
}