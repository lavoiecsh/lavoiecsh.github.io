using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day15Solver : Solver
    {
        public string ProblemName => "Beverage Bandits";

        private readonly DataProvider<Map> dataProvider;

        public Day15Solver(DataProvider<Map> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            throw new System.NotImplementedException();
        }

        public string SolveSecondPart()
        {
            throw new System.NotImplementedException();
        }

        public class Map
        {
            public readonly IReadOnlyList<Position> OpenTiles;
            public readonly IList<Unit> Units;

            public Map(IEnumerable<Position> openTiles, IEnumerable<Unit> units)
            {
                OpenTiles = openTiles.ToList();
                Units = units.ToList();
            }
        }

        public class Unit
        {
            public Position Position { get; }
            public readonly UnitType Type;
            public int HitPoints { get; }
            public int AttackPower { get; }

            public Unit(Position position, UnitType type)
            {
                Position = position;
                Type = type;
                HitPoints = 200;
                AttackPower = 3;
            }

            public enum UnitType
            {
                Elf,
                Goblin
            }

            internal void Move(Map map)
            {
                var nearestPosition = NearestPosition(map);
                var distances = new List<(Position P, int D)>
                {
                    (nearestPosition, 0)
                };
                var i = 0;
                while (i++ < distances.Count &&
                       distances.All(d => !Equals(d.P, Position)))
                {
                    var current = distances[i];
                    var above = current.P.Above();
                }
            }

            internal IEnumerable<Position> PositionsInRange(Map map)
            {
                var enemies = map.Units.Where(unit => unit.Type != Type);
                return enemies.SelectMany(enemy => new[]
                        {enemy.Position.Above(), enemy.Position.Below(), enemy.Position.Left(), enemy.Position.Right()})
                    .Where(p => map.OpenTiles.Contains(p) && map.Units.All(u => !Equals(u.Position, p)));
            }

            internal IEnumerable<Position> ReachablePositions(Map map)
            {
                var reachablePositions = new List<Position> {Position};
                for (var i = 0; i < reachablePositions.Count; ++i)
                {
                    var current = reachablePositions[i];
                    AddIfReachable(reachablePositions, map, current.Above());
                    AddIfReachable(reachablePositions, map, current.Below());
                    AddIfReachable(reachablePositions, map, current.Left());
                    AddIfReachable(reachablePositions, map, current.Right());
                }

                return PositionsInRange(map).Where(p => reachablePositions.Contains(p));
            }

            private static void AddIfReachable(ICollection<Position> reachablePositions, Map map, Position position)
            {
                if (reachablePositions.Contains(position)) return;
                if (map.OpenTiles.Contains(position) &&
                    map.Units.All(u => !Equals(u.Position, position)))
                    reachablePositions.Add(position);
            }

            internal Position NearestPosition(Map map)
            {
                return ReachablePositions(map).OrderByReading().ThenBy(p => p.Distance(Position)).First();
            }
        }

        public class Position
        {
            public readonly int X;
            public readonly int Y;

            private Position(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static implicit operator Position((int X, int Y) position)
            {
                return new Position(position.X, position.Y);
            }

            public Position Above() => new Position(X, Y - 1);
            public Position Below() => new Position(X, Y + 1);
            public Position Left() => new Position(X - 1, Y);
            public Position Right() => new Position(X + 1, Y);

            public int Distance(Position other)
            {
                return Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
            }

            private bool Equals(Position other)
            {
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((Position) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (X * 397) ^ Y;
                }
            }
        }
    }

    internal static class PositionEnumerableExtensions
    {
        internal static IOrderedEnumerable<Day15Solver.Position> OrderByReading(
            this IEnumerable<Day15Solver.Position> positions)
        {
            return positions.OrderBy(p => p.Y).ThenBy(p => p.X);
        }
    }
}