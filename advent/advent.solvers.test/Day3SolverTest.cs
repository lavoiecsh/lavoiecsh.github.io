using advent.util;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day3SolverTest
    {
        private readonly ISolver solver;

        private readonly Mock<IFileReader> fileReader;

        public Day3SolverTest()
        {
            fileReader = new Mock<IFileReader>();
            solver = new Day3Solver(fileReader.Object);
        }

        [Theory]
        [InlineData("4", "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2")]
        public void ReturnsNumberOfSquaresUsedMoreThanOnce(string expected, params string[] claims)
        {
            const string filename = "file.txt";
            fileReader.Setup(fr => fr.ReadStrings(filename)).Returns(claims);
            Assert.Equal(expected, solver.Solve1(new[] {filename}));
        }

        [Theory]
        [InlineData("3", "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2")]
        public void ReturnsClaimWithNoOverlap(string expected, params string[] claims)
        {
            const string filename = "file.txt";
            fileReader.Setup(fr => fr.ReadStrings(filename)).Returns(claims);
            Assert.Equal(expected, solver.Solve2(new[] {filename}));
        }
    }
}