using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day05SolverTest
    {
        private readonly Solver solver;

        public Day05SolverTest()
        {
            var dataProvider = new Mock<DataProvider<string>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(new[] {"dabAcCaCBAcCcaDA"});
            
            solver = new Day05Solver(dataProvider.Object);
        }

        [Fact]
        public void ReturnsLengthOfResultingPolymer()
        {
            Assert.Equal("10", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsLengthOfResultingImprovedPolymer()
        {
            Assert.Equal("4", solver.SolveSecondPart());
        }
    }
}