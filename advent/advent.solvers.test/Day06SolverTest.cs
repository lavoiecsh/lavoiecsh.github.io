using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day06SolverTest
    {
        private readonly Solver solver;

        public Day06SolverTest()
        {
            var dataProvider = new Mock<DataProvider<Day06Solver.Location>>();
            dataProvider.Setup(dp => dp.GetData())
                .Returns(new[]
                {
                    new Day06Solver.Location(1, 1),
                    new Day06Solver.Location(1, 6),
                    new Day06Solver.Location(8, 3),
                    new Day06Solver.Location(3, 4),
                    new Day06Solver.Location(5, 5),
                    new Day06Solver.Location(8, 9) 
                });
            
            solver = new Day06Solver(dataProvider.Object, 32);
        }

        [Fact]
        public void ReturnsSizeOfLargestFiniteArea()
        {
            Assert.Equal("17", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsSizeOfAreaCloseToAllLocations()
        {
            Assert.Equal("16", solver.SolveSecondPart());
        }
    }
}