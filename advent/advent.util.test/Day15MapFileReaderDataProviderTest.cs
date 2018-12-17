using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day15MapFileReaderDataProviderTest
    {
        private readonly Day15Solver.Map map;

        public Day15MapFileReaderDataProviderTest()
        {
            var dataProvider = new Day15MapFileReaderDataProvider("data\\day15_map.txt");
            map = dataProvider.GetData();
        }

        [Fact]
        public void ReturnsMapInFile()
        {
            var expectedOpenTiles = new List<Day15Solver.Position>
            {
                (1, 1), (2, 1), (3, 1), (4, 1), (5, 1),
                (1, 2), (2, 2), (3, 2), (4, 2), (5, 2),
                (1, 3), (3, 3), (5, 3),
                (1, 4), (2, 4), (3, 4), (5, 4),
                (1, 5), (2, 5), (3, 5), (4, 5), (5, 5)
            };
            Assert.Equal(expectedOpenTiles, map.OpenTiles, new PositionComparer());
        }

        [Fact]
        public void ReturnsUnitsInFile()
        {
            var expectedUnits = new List<Day15Solver.Unit>
            {
                new Day15Solver.Unit((2, 1), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((4, 2), Day15Solver.Unit.UnitType.Elf),
                new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((3, 4), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Elf)
            };
            Assert.Equal(expectedUnits, map.Units, new UnitComparer());
        }

        private class PositionComparer : IEqualityComparer<Day15Solver.Position>
        {
            public bool Equals(Day15Solver.Position x, Day15Solver.Position y)
            {
                return x.X == y.X && x.Y == y.Y;
            }

            public int GetHashCode(Day15Solver.Position obj)
            {
                throw new System.NotImplementedException();
            }
        }

        private class UnitComparer : IEqualityComparer<Day15Solver.Unit>
        {
            public bool Equals(Day15Solver.Unit x, Day15Solver.Unit y)
            {
                return x.Position.X == y.Position.X &&
                       x.Position.Y == y.Position.Y &&
                       x.Type == y.Type;
            }

            public int GetHashCode(Day15Solver.Unit obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}