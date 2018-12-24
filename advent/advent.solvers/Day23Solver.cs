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
            var minX = nanobots.Min(n => n.Position.X);
            var minY = nanobots.Min(n => n.Position.Y);
            var minZ = nanobots.Min(n => n.Position.Z);
            var maxX = nanobots.Max(n => n.Position.X);
            var maxY = nanobots.Max(n => n.Position.Y);
            var maxZ = nanobots.Max(n => n.Position.Z);
            var completeOctree = new OctreeBin(minX, minY, minZ, maxX, maxY, maxZ);
            var validBins = completeOctree.Children();
            while (validBins.Count != 1)
            {
                Console.WriteLine($"{validBins.Count}, First: {validBins.First()} with count = {validBins.First().CountInside(nanobots)}");
                var counts = validBins.ToDictionary(b => b, b => b.CountInside(nanobots));
                var maxCount = counts.Values.Max();
                validBins = counts.Where(kv => kv.Value == maxCount)
                    .SelectMany(kv => kv.Key.Children())
                    .ToList();
            }

            return validBins.Single().DistanceToOrigin().ToString();
        }

        private class OctreeBin
        {
            private readonly int minX;
            private readonly int minY;
            private readonly int minZ;
            private readonly int maxX;
            private readonly int maxY;
            private readonly int maxZ;

            public OctreeBin(int minX, int minY, int minZ, int maxX, int maxY, int maxZ)
            {
                this.minX = minX;
                this.minY = minY;
                this.minZ = minZ;
                this.maxX = maxX;
                this.maxY = maxY;
                this.maxZ = maxZ;
            }

            internal int DistanceToOrigin()
            {
                return maxX + maxY + maxZ;
            }

            internal IList<OctreeBin> XChildren()
            {
                if (minX == maxX) return new List<OctreeBin> {this};
                var halfX = (minX + maxX) / 2;
                return new List<OctreeBin>
                {
                    new OctreeBin(minX, minY, minZ, halfX, maxY, maxZ),
                    new OctreeBin(halfX + 1, minY, minZ, maxX, maxY, maxZ)
                };
            }

            internal IList<OctreeBin> YChildren()
            {
                if (minY == maxY) return new List<OctreeBin>{this};
                var halfY = (minY + maxY) / 2;
                return new List<OctreeBin>
                {
                    new OctreeBin(minX, minY, minZ, maxX, halfY, maxZ),
                    new OctreeBin(minX, halfY + 1, minZ, maxX, maxY, maxZ)
                };
            }

            internal IList<OctreeBin> ZChildren()
            {
                if (minZ == maxZ) return new List<OctreeBin> {this};
                var halfZ = (minZ + maxZ) / 2;
                return new List<OctreeBin>
                {
                    new OctreeBin(minX, minY, minZ, maxX, maxY, halfZ),
                    new OctreeBin(minX, minY, halfZ + 1, maxX, maxY, maxZ)
                };
            }

            internal IList<OctreeBin> Children()
            {
                if (minX == maxX &&
                    minY == maxY &&
                    minZ == maxZ)
                    return new List<OctreeBin> {this};

                if (minX != maxX)
                {
                    var halfX = (minX + maxX) / 2;
                    return new List<OctreeBin>
                    {
                        new OctreeBin(minX, minY, minZ, halfX, maxY, maxZ),
                        new OctreeBin(halfX + 1, minY, minZ, maxX, maxY, maxZ)
                    };
                }

                if (minY != maxY)
                {
                    var halfY = (minY + maxY) / 2;
                    return new List<OctreeBin>
                    {
                        new OctreeBin(minX, minY, minZ, maxX, halfY, maxZ),
                        new OctreeBin(minX, halfY + 1, minZ, maxX, maxY, maxZ)
                    };
                }

                var halfZ = (minZ + maxZ) / 2;
                return new List<OctreeBin>
                {
                    new OctreeBin(minX, minY, minZ, maxX, maxY, halfZ),
                    new OctreeBin(minX, minY, halfZ + 1, maxX, maxY, maxZ)
                };
                
//
//                var halfX = (minX + maxX) / 2;
//                var halfY = (minY + maxY) / 2;
//                var halfZ = (minZ + maxZ) / 2;
//
//                if (minX == maxX)
//                {
//                    if (minY == maxY)
//                        return new List<OctreeBin>
//                        {
//                            new OctreeBin(minX, minY, minZ, maxX, maxY, halfZ),
//                            new OctreeBin(minX, minY, halfZ + 1, maxX, maxY, maxZ)
//                        };
//                    return new List<OctreeBin>
//                    {
//                        new OctreeBin(minX, minY, minZ, maxX, halfY, halfZ),
//                        new OctreeBin(minX, minY, halfZ + 1, maxX, halfY, maxZ),
//                        new OctreeBin(minX, halfY + 1, minZ, maxX, maxY, halfZ),
//                        new OctreeBin(minX, halfY + 1, halfZ + 1, maxX, maxY, maxZ)
//                    };
//                }
//
//                if (minY == maxY)
//                {
//                    if (minZ == maxZ)
//                    {
//                        return new List<OctreeBin>
//                        {
//                            new OctreeBin(minX, minY, minZ, halfX, maxY, maxZ),
//                            new OctreeBin(halfX + 1, minY, minZ, maxX, maxY, maxZ)
//                        };
//                    }
//
//                    return new List<OctreeBin>
//                    {
//                        new OctreeBin(minX, minY, minZ, halfX, maxY, halfZ),
//                        new OctreeBin(minX, minY, halfZ + 1, halfX, maxY, maxZ),
//                        new OctreeBin(halfX + 1, minY, minZ, maxX, maxY, halfZ),
//                        new OctreeBin(halfX + 1, minY, halfZ + 1, maxX, maxY, maxZ)
//                    };
//                }
//
//                if (minZ == maxZ)
//                {
//                    return new List<OctreeBin>
//                    {
//                        new OctreeBin(minX, minY, minZ, halfX, halfY, maxZ),
//                        new OctreeBin(minX, halfY + 1, minZ, halfX, maxY, maxZ),
//                        new OctreeBin(halfX + 1, minY, minZ, maxX, halfY, maxZ),
//                        new OctreeBin(halfX + 1, halfY + 1, minZ, maxX, maxY, maxZ)
//                    };
//                }
//
//                return new List<OctreeBin>
//                {
//                    new OctreeBin(minX, minY, minZ, halfX, halfY, halfZ),
//                    new OctreeBin(minX, minY, halfZ + 1, halfX, halfY, maxZ),
//                    new OctreeBin(minX, halfY + 1, minZ, halfX, maxY, halfZ),
//                    new OctreeBin(minX, halfY + 1, halfZ + 1, halfX, maxY, maxZ),
//                    new OctreeBin(halfX + 1, minY, minZ, maxX, halfY, halfZ),
//                    new OctreeBin(halfX + 1, minY, halfZ + 1, maxX, halfY, maxZ),
//                    new OctreeBin(halfX + 1, halfY + 1, minZ, maxX, maxY, halfZ),
//                    new OctreeBin(halfX + 1, halfY + 1, halfZ + 1, maxX, maxY, maxZ)
//                };
            }

            internal int CountInside(IEnumerable<Nanobot> nanobots)
            {
                return nanobots.Count(Intersects);
            }

            private bool Intersects(Nanobot nanobot)
            {
                var d = nanobot.Radius;
                if (nanobot.Position.X < minX) d -= (minX - nanobot.Position.X);
                if (nanobot.Position.X > maxX) d -= (nanobot.Position.X - maxX);
                if (nanobot.Position.Y < minY) d -= (minY - nanobot.Position.Y);
                if (nanobot.Position.Y > maxY) d -= (nanobot.Position.Y - maxY);
                if (nanobot.Position.Z < minZ) d -= (minZ - nanobot.Position.Z);
                if (nanobot.Position.Z > maxZ) d -= (nanobot.Position.Z - maxZ);
                return d >= 0;
            }

            private bool Equals(OctreeBin other)
            {
                return minX == other.minX &&
                       minY == other.minY &&
                       minZ == other.minZ &&
                       maxX == other.maxX &&
                       maxY == other.maxY &&
                       maxZ == other.maxZ;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((OctreeBin) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = minX;
                    hashCode = (hashCode * 397) ^ minY;
                    hashCode = (hashCode * 397) ^ minZ;
                    hashCode = (hashCode * 397) ^ maxX;
                    hashCode = (hashCode * 397) ^ maxY;
                    hashCode = (hashCode * 397) ^ maxZ;
                    return hashCode;
                }
            }

            public override string ToString()
            {
                return $"{minX},{minY},{minZ} to {maxX},{maxY},{maxZ}";
            }
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
        }
    }
}