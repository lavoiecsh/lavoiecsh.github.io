using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var map = dataProvider.GetData();
            var rounds = map.Play();
            var unitHitPoints = map.Units.Sum(u => u.HitPoints);
            return (rounds * unitHitPoints).ToString();
        }

        public string SolveSecondPart()
        {
            var map = dataProvider.GetData();
            var (_, rounds) = map.FindWinningAttack();
            var unitHitPoints = map.Units.Sum(u => u.HitPoints);
            return (rounds * unitHitPoints).ToString();
        }

        public class Map
        {
            public readonly IReadOnlyList<Position> OpenTiles;
            public IList<Unit> Units { get; private set; }

            private readonly int minX;
            private readonly int minY;
            private readonly int maxX;
            private readonly int maxY;

            public Map(IEnumerable<Position> openTiles, IEnumerable<Unit> units)
            {
                OpenTiles = openTiles.ToList();
                Units = units.ToList();

                minX = OpenTiles.Min(t => t.X) - 1;
                minY = OpenTiles.Min(t => t.Y) - 1;
                maxX = OpenTiles.Max(t => t.X) + 1;
                maxY = OpenTiles.Max(t => t.Y) + 1;
            }

            public (int Attack, int Rounds) FindWinningAttack()
            {
                var unitsBefore = Units;
                var elfCount = Units.Count(u => u.Type == Unit.UnitType.Elf);
                for (var attack = 4; attack < 1000; ++attack)
                {
                    Units = unitsBefore.Select(u => u.CloneWithIncreasedAttack(attack)).ToList();
                    var rounds = Play();
                    if (Units.Count(u => u.Type == Unit.UnitType.Elf) == elfCount) return (attack, rounds);
                }

                return (-1, -1);
            }

            public int Play()
            {
                var rounds = 0;
                while (Units.Any(u => u.Type == Unit.UnitType.Elf) &&
                       Units.Any(u => u.Type == Unit.UnitType.Goblin))
                {
                    var roundFinished = PlayRound();
                    if (roundFinished) rounds++;
                }

                return rounds;
            }

            internal bool PlayRound()
            {
                var unitsPlayed = 0;
                var unitsToPlay = Units.OrderByReading().ToList();
                foreach (var unit in unitsToPlay)
                {
                    unit.Play(this);
                    unitsPlayed++;
                    if (Units.All(u => u.Type == Unit.UnitType.Elf) ||
                        Units.All(u => u.Type == Unit.UnitType.Goblin))
                        break;
                }

                return unitsPlayed == unitsToPlay.Count;
            }

            public bool IsOpen(Position p)
            {
                return OpenTiles.Contains(p) && Units.All(u => !Equals(u.Position, p));
            }

            public override string ToString()
            {
                var builder = new StringBuilder();
                for (var y = minY; y <= maxY; ++y)
                {
                    for (var x = minX; x <= maxX; ++x)
                    {
                        var position = (Position) (x, y);
                        var unit = Units.SingleOrDefault(u => Equals(u.Position, position));
                        if (unit != null)
                            builder.Append(unit.Type == Unit.UnitType.Elf ? 'E' : 'G');
                        else
                            builder.Append(OpenTiles.Contains(position) ? '.' : '#');
                    }

                    var y1 = y;
                    var unitsInLine = Units.Where(u => u.Position.Y == y1).OrderBy(u => u.Position.X);
                    foreach (var unit in unitsInLine)
                    {
                        var type = unit.Type == Unit.UnitType.Elf ? 'E' : 'G';
                        builder.Append($" {type}({unit.HitPoints})");
                    }

                    builder.AppendLine();
                }

                return builder.ToString();
            }
        }

        public class Unit
        {
            public Position Position { get; private set; }
            public readonly UnitType Type;
            public int HitPoints { get; internal set; }
            private int AttackPower { get; set; }

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

            public void Play(Map map)
            {
                if (HitPoints <= 0) return;
                var attacked = Attack(map);
                if (attacked) return;
                Move(map);
                Attack(map);
            }

            private bool Attack(Map map)
            {
                var adjacentUnits = Position.Adjacent()
                    .Select(p => map.Units.FirstOrDefault(u => Equals(p, u.Position) && u.Type != Type))
                    .Where(u => u != null)
                    .ToList();

                if (!adjacentUnits.Any()) return false;

                var lowestHitPoints = adjacentUnits.Min(u => u.HitPoints);
                var lowestHitPointUnits = adjacentUnits.Where(u => u.HitPoints == lowestHitPoints).ToList();
                Attack(map, lowestHitPointUnits.OrderByReading().First());
                return true;
            }

            private void Attack(Map map, Unit unit)
            {
                unit.HitPoints -= AttackPower;
                if (unit.HitPoints <= 0)
                    map.Units.Remove(unit);
            }

            internal void Move(Map map)
            {
                var nearestPosition = NearestPosition(map);
                if (nearestPosition == null) return;
                var distances = new List<(Position P, int D)>
                {
                    (nearestPosition, 0)
                };
                var i = -1;
                while (++i < distances.Count)
                {
                    var (position, distance) = distances[i];
                    var adjacentPositions = position.Adjacent().ToList();
                    foreach (var adjacentPosition in adjacentPositions)
                    {
                        if (distances.All(d => !Equals(d.P, adjacentPosition)) &&
                            map.IsOpen(adjacentPosition))
                            distances.Add((adjacentPosition, distance + 1));
                    }
                }

                var possiblePositions = Position.Adjacent()
                    .Select(a => distances.SingleOrDefault(d => Equals(d.P, a)))
                    .Where(d => d.P != null)
                    .OrderBy(d => d.D)
                    .ToList();
                var nearestPositions = possiblePositions.TakeWhile(d => d.D == possiblePositions.First().D);
                Position = nearestPositions.Select(d => d.P).OrderByReading().First();
            }

            internal Position NearestPosition(Map map)
            {
                var reachablePositions = ReachablePositions(map).ToList();
                if (!reachablePositions.Any()) return null;
                var distances = new List<(Position P, int D)>
                {
                    (Position, 0)
                };
                var i = -1;
                while (++i < distances.Count)
                {
                    var (position, distance) = distances[i];
                    var adjacentPositions = position.Adjacent().ToList();
                    foreach (var adjacentPosition in adjacentPositions)
                    {
                        if (distances.All(d => !Equals(d.P, adjacentPosition)) &&
                            map.IsOpen(adjacentPosition))
                            distances.Add((adjacentPosition, distance + 1));
                    }
                }

                var reachableDistances = reachablePositions.Select(p => distances.Single(d => Equals(d.P, p))).ToList();
                var minDistance = reachableDistances.Min(d => d.D);
                var nearestPositions = reachableDistances.Where(d => d.D == minDistance).Select(d => d.P);
                return nearestPositions.OrderByReading().First();
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
                // ReSharper disable once ForCanBeConvertedToForeach
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

            public override string ToString()
            {
                return $"{Type} {HitPoints} {Position}";
            }

            public Unit CloneWithIncreasedAttack(int attack)
            {
                return new Unit((Position.X, Position.Y), Type)
                {
                    AttackPower = Type == UnitType.Elf ? attack : 3
                };
            }
        }

        public class Position
            : IComparable<Position>, IComparable
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

            public IEnumerable<Position> Adjacent()
            {
                return new List<Position> {Above(), Left(), Right(), Below()};
            }

            public override string ToString()
            {
                return $"({X},{Y})";
            }

            public int CompareTo(Position other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                var xComparison = X.CompareTo(other.X);
                return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
            }

            public int CompareTo(object obj)
            {
                if (ReferenceEquals(null, obj)) return 1;
                if (ReferenceEquals(this, obj)) return 0;
                return obj is Position other
                    ? CompareTo(other)
                    : throw new ArgumentException($"Object must be of type {nameof(Position)}");
            }
        }
    }

    internal static class PositionEnumerableExtensions
    {
        internal static IOrderedEnumerable<Day15Solver.Unit> OrderByReading(this IEnumerable<Day15Solver.Unit> units)
        {
            return units.OrderBy(u => u.Position.Y).ThenBy(u => u.Position.X);
        }

        internal static IOrderedEnumerable<Day15Solver.Position> OrderByReading(
            this IEnumerable<Day15Solver.Position> positions)
        {
            return positions.OrderBy(p => p.Y).ThenBy(p => p.X);
        }
    }
}