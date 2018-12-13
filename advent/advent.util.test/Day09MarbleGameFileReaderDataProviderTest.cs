using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day09MarbleGameFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsMarbleGamesFromFile()
        {
            const string filename = "data\\day09_marble_game.txt";
            var expected = new Day09Solver.MarbleGame(10, 1618);
            var dp = new Day09MarbleGameFileReaderDataProvider(filename);
            Assert.Equal(expected, dp.GetData(), new MarbleGameComparer());
        }

        private class MarbleGameComparer : IEqualityComparer<Day09Solver.MarbleGame>
        {
            public bool Equals(Day09Solver.MarbleGame x, Day09Solver.MarbleGame y)
            {
                return x.PlayerCount == y.PlayerCount &&
                       x.LastMarble == y.LastMarble;
            }

            public int GetHashCode(Day09Solver.MarbleGame obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}