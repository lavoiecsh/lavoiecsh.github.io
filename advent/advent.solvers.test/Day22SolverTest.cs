using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day22SolverTest
    {
        private readonly Day22Solver.Maze maze;
        private readonly Solver solver;

        public Day22SolverTest()
        {
            maze = new Day22Solver.Maze(510, (10, 10));

            var dataProvider = new Mock<DataProvider<Day22Solver.Maze>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(maze);
            
            solver = new Day22Solver(dataProvider.Object);
        }
        
        [Theory]
        [InlineData(0, 0, Day22Solver.Maze.RegionType.Rocky)]
        [InlineData(1, 0, Day22Solver.Maze.RegionType.Wet)]
        [InlineData(0, 1, Day22Solver.Maze.RegionType.Rocky)]
        [InlineData(1, 1, Day22Solver.Maze.RegionType.Narrow)]
        [InlineData(10, 10, Day22Solver.Maze.RegionType.Rocky)]
        public void ReturnsGeologyIndexOfRegion(int x, int y, Day22Solver.Maze.RegionType expectedGeologyIndex)
        {
            Assert.Equal(expectedGeologyIndex, maze.GetRegionType(x, y));
        }

        [Fact]
        public void ReturnsRiskLevelOfCave()
        {
            Assert.Equal("114", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsTimeToTarget()
        {
            Assert.Equal("45", solver.SolveSecondPart());
        }
    }
}