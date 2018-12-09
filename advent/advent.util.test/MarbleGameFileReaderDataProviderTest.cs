using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class MarbleGameFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsMarbleGamesFromFile()
        {
            const string filename = "data\\marble_game_list.txt";
            var expected = new List<Day09Solver.MarbleGame>
            {
                new Day09Solver.MarbleGame(10, 1618),
                new Day09Solver.MarbleGame(13, 7999),
                new Day09Solver.MarbleGame(17, 1104),
                new Day09Solver.MarbleGame(21, 6111),
                new Day09Solver.MarbleGame(30, 5807)
            };
            var dp = new MarbleGameFileReaderDataProvider(filename);
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