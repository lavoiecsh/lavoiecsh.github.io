using System.Collections.Generic;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day02SolverTest
    {
        private readonly Solver solver;

        private readonly Mock<DataProvider<IEnumerable<string>>> dataProvider;

        public Day02SolverTest()
        {
            dataProvider = new Mock<DataProvider<IEnumerable<string>>>();
            solver = new Day02Solver(dataProvider.Object);
        }

        [Theory]
        [InlineData("12", "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdeee", "ababab")]
        public void ReturnsChecksum(string expected, params string[] ids)
        {
            dataProvider.Setup(fr => fr.GetData()).Returns(ids);
            Assert.Equal(expected, solver.SolveFirstPart());
        }

        [Theory]
        [InlineData("fgij", "abcde", "fghij", "klmno", "pqrst", "fguij", "axcye", "wvxyz")]
        public void ReturnsCommonLettersOfMatchingIds(string expected, params string[] ids)
        {
            dataProvider.Setup(fr => fr.GetData()).Returns(ids);
            Assert.Equal(expected, solver.SolveSecondPart());
        }
    }
}