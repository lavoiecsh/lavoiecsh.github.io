using System.Collections.Generic;
using Xunit;

namespace advent.solvers.test
{
    public class Day15SolverTest
    {
        private readonly Day15Solver.Map map;
        private readonly Day15Solver.Map sample1;
        private readonly Day15Solver.Map sample2;

        public Day15SolverTest()
        {
            var openTiles = new List<Day15Solver.Position>
            {
                (1, 1), (2, 1), (3, 1), (4, 1), (5, 1),
                (1, 2), (2, 2), (3, 2), (4, 2), (5, 2),
                (1, 3), (3, 3), (5, 3),
                (1, 4), (2, 4), (3, 4), (5, 4),
                (1, 5), (2, 5), (3, 5), (4, 5), (5, 5)
            };
            var units = new List<Day15Solver.Unit>
            {
                new Day15Solver.Unit((2, 1), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((4, 2), Day15Solver.Unit.UnitType.Elf),
                new Day15Solver.Unit((5, 2), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((3, 4), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((5, 4), Day15Solver.Unit.UnitType.Elf)
            };
            map = new Day15Solver.Map(openTiles, units);
            
            sample1 = new Day15Solver.Map(new List<Day15Solver.Position>
            {
                (1, 1), (2, 1), (3, 1), (4, 1), (5, 1),
                (1, 2), (2, 2), (3, 2), (5, 2),
                (1, 3), (2, 3), (3, 3), (5, 3)
            }, new List<Day15Solver.Unit>
            {
                new Day15Solver.Unit((1, 1), Day15Solver.Unit.UnitType.Elf),
                new Day15Solver.Unit((4, 1), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((2, 3), Day15Solver.Unit.UnitType.Goblin),
                new Day15Solver.Unit((5, 3), Day15Solver.Unit.UnitType.Goblin)
            });

            sample2 = new Day15Solver.Map(new List<Day15Solver.Position>
            {
                (1, 1), (2, 1), (3, 1), (4, 1), (5, 1),
                (1, 2), (2, 2), (3, 2), (4, 2), (5, 2),
                (1, 3), (2, 3), (3, 3), (4, 3), (5, 3)
            }, new List<Day15Solver.Unit>
            {
                new Day15Solver.Unit((1, 1), Day15Solver.Unit.UnitType.Elf),
                new Day15Solver.Unit((4, 1), Day15Solver.Unit.UnitType.Goblin),
            });
        }

        [Fact]
        public void ReturnsPositionsInRange()
        {
            var inRange = sample1.Units[0].PositionsInRange(sample1);
            var expectedInRange = new List<Day15Solver.Position>
            {
                (3, 1), (5, 1),
                (2, 2), (5, 2),
                (1, 3), (3, 3)
            };
            Assert.Equal(expectedInRange.OrderByReading(), inRange.OrderByReading());
        }

        [Fact]
        public void ReturnsReachablePositions()
        {
            var reachable = sample1.Units[0].ReachablePositions(sample1);
            var expectedReachable = new List<Day15Solver.Position>
            {
                (3, 1),
                (2, 2),
                (1, 3), (3, 3)
            };
            Assert.Equal(expectedReachable.OrderByReading(),reachable.OrderByReading());
        }

        [Fact]
        public void ReturnsNearestPosition()
        {
            var nearest = sample1.Units[0].NearestPosition(sample1);
            var expectedNearest = (3, 1);
            Assert.Equal(expectedNearest, nearest);
        }

        [Fact]
        public void MovesTowardsNearestPosition()
        {
            sample1.Units[0].Move(sample1);
            Assert.Equal((2, 1), sample1.Units[0].Position);
        }
    }
}