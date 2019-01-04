using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day20SolverTest
    {
        private readonly Mock<DataProvider<string>> dataProvider;
        private readonly Solver solver;

        public Day20SolverTest()
        {
            dataProvider = new Mock<DataProvider<string>>();
            solver = new Day20Solver(dataProvider.Object);
        }

        [Theory]
        [InlineData(3, "^WNE$")]
        [InlineData(10, "^ENWWW(NEEE|SSE(EE|N))$")]
        [InlineData(18, "^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$")]
        [InlineData(23, "^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$")]
        [InlineData(31, "^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$")]
        public void ReturnsLargestNumberOfDoorsRequiredToPassThroughEachRoom(int expectedNumber, string regex)
        {
            dataProvider.Setup(dp => dp.GetData()).Returns(regex);
            Assert.Equal(expectedNumber.ToString(), solver.SolveFirstPart());
        }
    }
}
