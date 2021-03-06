﻿using System;
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
            Console.WriteLine($"Solving problem {day}-{problem} ({solver.ProblemName}) at {DateTime.Now}");
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
                case 12:
                    return new Day12Solver(new Day12PlantCavernFileReaderDataProvider(args.First()));
                case 13:
                    return new Day13Solver(new Day13CartMapFileReaderDataProvider(args.First()));
                case 14:
                    return new Day14Solver(args.First());
                case 15:
                    return new Day15Solver(new Day15MapFileReaderDataProvider(args.First()));
                case 16:
                    var dataProvider = new Day16ProcessorFileReaderDataProvider(args.First());
                    return new Day16Solver(dataProvider, dataProvider);
                case 17:
                    return new Day17Solver(new Day17ClayFileReaderDataProvider(args.First()));
                case 18:
                    return new Day18Solver(new Day18LumberMapFileReaderDataProvider(args.First()));
                case 19:
                    return new Day19Solver(new Day19ProcessorInstructionFileReaderDataProvider(args.First()));
                case 20:
                    return new Day20Solver(new SimpleFileReaderDataProvider(args.First()));
                case 21:
                    return new Day21Solver(new Day19ProcessorInstructionFileReaderDataProvider(args.First()));
                case 22:
                    return new Day22Solver(new Day22MazeFileReaderDataProvider(args.First()));
                case 23:
                    return new Day23Solver(new Day23NanobotsFileReaderDataProvider(args.First()));
                case 24:
                    return new Day24Solver(new Day24ImmuneCombatFileReaderDataProvider(args.First()));
                case 25:
                    return new Day25Solver(new Day25PointFileReaderDataProvider(args.First()));
                default:
                    return null;
            }
        }
    }
}