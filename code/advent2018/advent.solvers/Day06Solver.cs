using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day06Solver : Solver
    {
        public string ProblemName => "Chronal Coordinates";

        private readonly DataProvider<IList<Location>> dataProvider;
        private readonly int maxDistanceSumAllowed;

        public Day06Solver(DataProvider<IList<Location>> dataProvider, int maxDistanceSumAllowed = 10000)
        {
            this.dataProvider = dataProvider;
            this.maxDistanceSumAllowed = maxDistanceSumAllowed;
        }

        public string SolveFirstPart()
        {
            var locations = dataProvider.GetData();
            var maxX = locations.Max(l => l.X);
            var maxY = locations.Max(l => l.Y);
            for (var i = 0; i <= maxX; ++i)
            {
                for (var j = 0; j <= maxY; ++j)
                {
                    var tmpLocation = new Location(i, j);
                    var closestLocations = locations
                        .Select(l => new {Distance = l.DistanceTo(tmpLocation), Location = l})
                        .OrderBy(l => l.Distance)
                        .ToList();
                    if (closestLocations[1].Distance == closestLocations[0].Distance)
                        continue;
                    closestLocations[0].Location.AddLocationToArea(tmpLocation);
                }
            }

            return locations.Where(l => l.HasFiniteArea(maxX, maxY)).Max(l => l.AreaSize()).ToString();
        }

        public string SolveSecondPart()
        {
            var locations = dataProvider.GetData();
            var maxX = locations.Max(l => l.X);
            var maxY = locations.Max(l => l.Y);
            var locationsInArea = 0;
            for (var i = 0; i < maxX; i++)
            {
                for (var j = 0; j < maxY; j++)
                {
                    var tmpLocation = new Location(i, j);
                    var distanceSum = locations.Sum(l => l.DistanceTo(tmpLocation));
                    if (distanceSum < maxDistanceSumAllowed)
                        locationsInArea++;
                }
            }

            return locationsInArea.ToString();
        }

        public class Location
        {
            public readonly int X;
            public readonly int Y;

            private readonly IList<Location> locationsInArea;

            public Location(int x, int y)
            {
                X = x;
                Y = y;
                locationsInArea = new List<Location>();
            }

            public int DistanceTo(Location target)
            {
                return Math.Abs(X - target.X) + Math.Abs(Y - target.Y);
            }

            public void AddLocationToArea(Location location)
            {
                locationsInArea.Add(location);
            }

            public bool HasFiniteArea(int maxX, int maxY)
            {
                return !locationsInArea.Any(location => IsBorder(location.X, location.Y, maxX, maxY));
            }

            private static bool IsBorder(int x, int y, int maxX, int maxY)
            {
                return x == 0 || y == 0 || x == maxX || y == maxY;
            }

            public int AreaSize()
            {
                return locationsInArea.Count;
            }
        }
    }
}