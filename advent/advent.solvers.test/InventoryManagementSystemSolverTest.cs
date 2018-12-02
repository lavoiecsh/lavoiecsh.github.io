using advent.util;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class InventoryManagementSystemSolverTest
    {
        private readonly ISolver solver;

        private readonly Mock<IFileReader> fileReader;

        public InventoryManagementSystemSolverTest()
        {
            fileReader = new Mock<IFileReader>();
            solver = new InventoryManagementSystemSolver(fileReader.Object);
        }

        [Theory]
        [InlineData("12", "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdeee", "ababab")]
        public void ReturnsChecksum(string expected, params string[] ids)
        {
            const string filename = "file.txt";
            fileReader.Setup(fr => fr.ReadStrings(filename)).Returns(ids);
            Assert.Equal(expected, solver.Solve1(new[] {filename}));
        }

        [Theory]
        [InlineData("fgij", "abcde", "fghij", "klmno", "pqrst", "fguij", "axcye", "wvxyz")]
        public void ReturnsCommonLettersOfMatchingIds(string expected, params string[] ids)
        {
            const string filename = "file.txt";
            fileReader.Setup(fr => fr.ReadStrings(filename)).Returns(ids);
            Assert.Equal(expected, solver.Solve2(new[] {filename}));
        }
    }
}