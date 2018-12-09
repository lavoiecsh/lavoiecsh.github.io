using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day09Solver : Solver
    {
        public string ProblemName => "Marble Mania";

        private readonly DataProvider<MarbleGame> dataProvider;

        public Day09Solver(DataProvider<MarbleGame> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var game = dataProvider.GetData().First();
            game.PlayGame();
            return game.HighestScore().ToString();
        }

        public string SolveSecondPart()
        {
            var game = dataProvider.GetData().First();
            game.LastMarble *= 100;
            game.PlayGame();
            return game.HighestScore().ToString();
        }

        public class MarbleGame
        {
            public readonly int PlayerCount;
            public int LastMarble;

            private readonly long[] scores;
            private readonly List<int> marbles;
            private int currentMarbleIndex;
            private int currentPlayer;

            public MarbleGame(int playerCount, int lastMarble)
            {
                PlayerCount = playerCount;
                LastMarble = lastMarble;
                scores = new long[playerCount];
                marbles = new List<int>(lastMarble) {0};
            }

            public void PlayGame()
            {
                currentPlayer = 0;
                for (var i = 1; i <= LastMarble; ++i)
                {
                    if (i % 23 == 0)
                        RemoveMarble(i);
                    else
                        PlaceMarble(i);
                    currentPlayer++;
                    if (currentPlayer == PlayerCount)
                        currentPlayer = 0;
                }
            }

            private void RemoveMarble(int marble)
            {
                currentMarbleIndex -= 7;
                if (currentMarbleIndex < 0)
                    currentMarbleIndex += marbles.Count;
                scores[currentPlayer] += marble + marbles[currentMarbleIndex];
                marbles.RemoveAt(currentMarbleIndex);
            }

            private void PlaceMarble(int marble)
            {
                currentMarbleIndex += 2;
                if (currentMarbleIndex > marbles.Count)
                    currentMarbleIndex -= marbles.Count;
                marbles.Insert(currentMarbleIndex, marble);
            }

            public long HighestScore()
            {
                return scores.Max();
            }
        }
    }
}