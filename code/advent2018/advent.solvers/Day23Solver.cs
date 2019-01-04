using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day23Solver : Solver
    {
        public string ProblemName => "Experimental Emergency Teleportation";

        private readonly DataProvider<IList<Nanobot>> dataProvider;

        public Day23Solver(DataProvider<IList<Nanobot>> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var nanobots = dataProvider.GetData();
            var largestRadius = nanobots.OrderByDescending(n => n.Radius).First();
            return nanobots.Count(n => largestRadius.IsInRangeOf(n)).ToString();
        }

        public string SolveSecondPart()
        {
            var nanobots = dataProvider.GetData();

            var queue = new List<(int, int)>();
            foreach (var nanobot in nanobots)
            {
                var d = nanobot.DistanceToOrigin();
                queue.Add((Math.Max(0, d - nanobot.Radius), 1));
                queue.Add((d + nanobot.Radius + 1, -1));
            }

            var count = 0;
            var maxCount = 0;
            var result = 0;
            queue = queue.OrderBy(kv => kv.Item1).ToList();
            while (queue.Any())
            {
                var (d, e) = queue.First();
                queue.RemoveAt(0);
                count += e;
                if (count <= maxCount) continue;
                result = d;
                maxCount = count;
            }

            return result.ToString();
        }

        public class Nanobot
        {
            public (int X, int Y, int Z) Position;
            public readonly int Radius;

            public Nanobot(int x, int y, int z, int r)
            {
                Position = (x, y, z);
                Radius = r;
            }

            public bool IsInRangeOf(Nanobot nanobot)
            {
                return DistanceTo(nanobot) <= Radius;
            }

            private int DistanceTo(Nanobot nanobot)
            {
                return Math.Abs(nanobot.Position.X - Position.X) +
                       Math.Abs(nanobot.Position.Y - Position.Y) +
                       Math.Abs(nanobot.Position.Z - Position.Z);
            }

            public override string ToString()
            {
                return $"{Position.X} {Position.Y} {Position.Z} with radius {Radius}";
            }

            public int DistanceToOrigin()
            {
                return Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
            }
        }
    }
}