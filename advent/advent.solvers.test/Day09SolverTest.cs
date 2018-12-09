using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day09SolverTest
    {
        private readonly Solver solver;

        private readonly Mock<DataProvider<Day09Solver.MarbleGame>> dataProvider;
        
        public Day09SolverTest()
        {
            dataProvider = new Mock<DataProvider<Day09Solver.MarbleGame>>();
            
            solver = new Day09Solver(dataProvider.Object);
        }

        [Theory]
        [InlineData(10, 1618, 8317)]
        [InlineData(13, 7999, 146373)]
        [InlineData(17, 1104, 2764)]
        [InlineData(21, 6111, 54718)]
        [InlineData(30, 5807, 37305)]
        public void ReturnsHighestScore(int playerCount, int lastMarble, int expectedScore)
        {
            dataProvider.Setup(dp => dp.GetData())
                .Returns(new[] {new Day09Solver.MarbleGame(playerCount, lastMarble)});
            
            Assert.Equal(expectedScore.ToString(), solver.SolveFirstPart());
        }
        
        
    }
}