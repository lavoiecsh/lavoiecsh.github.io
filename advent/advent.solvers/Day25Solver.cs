using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day25Solver : Solver
    {
        public string ProblemName => "Four-Dimenstional Adventure";

        private readonly DataProvider<IList<Point>> dataProvider;

        public Day25Solver(DataProvider<IList<Point>> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var points = dataProvider.GetData();
            var constellations = new List<Constellation>();
            foreach (var point in points)
            {
                var constellation = constellations.FirstOrDefault(c => c.IsPartOfConstellation(point));
                if (constellation == null)
                {
                    constellation = new Constellation();
                    constellations.Add(constellation);
                }

                constellation.AddPoint(point);
            }

            var merge = true;
            while (merge)
            {
                merge = false;
                for (var i = 0; i < constellations.Count; ++i)
                {
                    for (var j = i + 1; j < constellations.Count; ++j)
                    {
                        if (!constellations[i].CanMergeWith(constellations[j]))
                            continue;
                        merge = true;
                        constellations[i].MergeWith(constellations[j]);
                        constellations.RemoveAt(j);
                        break;
                    }

                    if (merge)
                        break;
                }
            }

            return constellations.Count.ToString();
        }

        public string SolveSecondPart()
        {
            throw new System.NotImplementedException();
        }

        private class Constellation
        {
            private readonly IList<Point> points;

            public Constellation()
            {
                points = new List<Point>();
            }

            public bool IsPartOfConstellation(Point point)
            {
                return points.Any(p => p.DistanceFrom(point) <= 3);
            }

            public void AddPoint(Point point)
            {
                points.Add(point);
            }

            public bool CanMergeWith(Constellation other)
            {
                return points.Any(other.IsPartOfConstellation);
            }

            public void MergeWith(Constellation other)
            {
                foreach (var point in other.points) points.Add(point);
            }
            
        }

        public class Point
        {
            public readonly int X;
            public readonly int Y;
            public readonly int Z;
            public readonly int W;

            public Point(int x, int y, int z, int w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }

            public int DistanceFrom(Point other)
            {
                return Math.Abs(other.X - X) + Math.Abs(other.Y - Y) + Math.Abs(other.Z - Z) + Math.Abs(other.W - W);
            }
        }
    }
}