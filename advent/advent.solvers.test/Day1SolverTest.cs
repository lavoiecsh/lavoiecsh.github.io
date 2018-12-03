using advent.util;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day1SolverTest
    {
        private readonly ISolver solver;

        private readonly Mock<IFileReader> fileReader;

        public Day1SolverTest()
        {
            fileReader = new Mock<IFileReader>();
            solver = new Day1Solver(fileReader.Object);
        }

        [Theory]
        [InlineData("3", 1, 1, 1)]
        [InlineData("0", 1, 1, -2)]
        [InlineData("-6", -1, -2, -3)]
        public void ReturnsFrequency(string expected, params int[] changes)
        {
            const string filename = "file.txt";
            fileReader.Setup(fr => fr.ReadInts(filename)).Returns(changes);
            Assert.Equal(expected, solver.Solve1(new[] {filename}));
        }

        [Theory]
        [InlineData("0", 1, -1)]
        [InlineData("10", 3, 3, 4, -2, -4)]
        [InlineData("5", -6, 3, 8, 5, -6)]
        [InlineData("14", 7, 7, -2, -7, -4)]
        public void ReturnsFirstDuplicateFrequency(string expected, params int[] changes)
        {
            const string filename = "file.txt";
            fileReader.Setup(fr => fr.ReadInts(filename)).Returns(changes);
            Assert.Equal(expected, solver.Solve2(new[] {filename}));
        }
    }
}