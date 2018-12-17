using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day15Solver : Solver
    {
        public string ProblemName => "Beverage Bandits";

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
            public Position Position { get; private set; }
            public readonly UnitType Type;
            public int HitPoints { get; private set; }
            public int AttackPower { get; private set; }

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

            internal IEnumerable<Position> PositionsInRange(Map map)
            {
                var enemies = map.Units.Where(unit => unit.Type != Type);
                return enemies.SelectMany(enemy => new[]
                        {enemy.Position.Above(), enemy.Position.Below(), enemy.Position.Left(), enemy.Position.Right()})
                    .Where(p => map.OpenTiles.Contains(p) && map.Units.All(u => !Equals(u.Position, p)));
            }

            public IEnumerable<Position> ReachablePositions(Map map)
            {
                throw new NotImplementedException();
            }

            public void Move(Position position)
            {
                Position = position;
            }

            public int[,] MovementMap(Map map)
            {
                throw new NotImplementedException();
            }
        }

        public class Position
        {
            public readonly int X;
            public readonly int Y;

            public Position(int x, int y)
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
        }
    }
}