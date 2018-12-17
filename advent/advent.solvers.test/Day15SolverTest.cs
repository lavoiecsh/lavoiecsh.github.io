using System.Collections.Generic;
using Xunit;

namespace advent.solvers.test
{
    public class Day15SolverTest
    {
        private readonly Day15Solver.Map map;

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
        }

        [Fact]
        public void ReturnsPositionsInRange()
        {
            var inRange = map.Units[0].PositionsInRange(map);
            var expectedInRange = new List<Day15Solver.Position> {(4, 1), (3, 2), (5, 5)};
            Assert.Equal(expectedInRange, inRange);
        }

        [Fact]
        public void ReturnsReachablePositions()
        {
            map.Units[5].Move(map.Units[5].Position.Below());
            var reachable = map.Units[1].ReachablePositions(map);
            var expectedReachable = new List<Day15Solver.Position> {(4, 1), (3, 2)};
            Assert.Equal(expectedReachable, reachable);
        }
    }
}