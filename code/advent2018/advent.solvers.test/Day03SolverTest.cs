using System.Collections.Generic;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day03SolverTest
    {
        private readonly Solver solver;

        public Day03SolverTest()
        {
            var claims = new[]
            {
                new Day03Solver.Claim(1, 1, 3, 4, 4),
                new Day03Solver.Claim(2, 3, 1, 4, 4),
                new Day03Solver.Claim(3, 5, 5, 2, 2)
            };
            var dataProvider = new Mock<DataProvider<IEnumerable<Day03Solver.Claim>>>();
            dataProvider.Setup(fr => fr.GetData()).Returns(claims);

            solver = new Day03Solver(dataProvider.Object);
        }

        [Fact]
        public void ReturnsNumberOfSquaresUsedMoreThanOnce()
        {
            Assert.Equal("4", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsClaimWithNoOverlap()
        {
            Assert.Equal("3", solver.SolveSecondPart());
        }
    }
}