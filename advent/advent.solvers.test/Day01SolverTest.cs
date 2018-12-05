using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day01SolverTest
    {
        private readonly Solver solver;

        private readonly Mock<DataProvider<int>> dataProvider;

        public Day01SolverTest()
        {
            dataProvider = new Mock<DataProvider<int>>();
            solver = new Day01Solver(dataProvider.Object);
        }

        [Theory]
        [InlineData("3", 1, 1, 1)]
        [InlineData("0", 1, 1, -2)]
        [InlineData("-6", -1, -2, -3)]
        public void ReturnsFrequency(string expected, params int[] changes)
        {
            dataProvider.Setup(fr => fr.GetData()).Returns(changes);
            Assert.Equal(expected, solver.SolveFirstPart());
        }

        [Theory]
        [InlineData("0", 1, -1)]
        [InlineData("10", 3, 3, 4, -2, -4)]
        [InlineData("5", -6, 3, 8, 5, -6)]
        [InlineData("14", 7, 7, -2, -7, -4)]
        public void ReturnsFirstDuplicateFrequency(string expected, params int[] changes)
        {
            dataProvider.Setup(fr => fr.GetData()).Returns(changes);
            Assert.Equal(expected, solver.SolveSecondPart());
        }
    }
}