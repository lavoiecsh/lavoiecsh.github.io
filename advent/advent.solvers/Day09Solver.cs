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
            private int currentPlayer;

            private readonly LinkedList<int> marbles;
            private LinkedListNode<int> currentMarble;

            public MarbleGame(int playerCount, int lastMarble)
            {
                PlayerCount = playerCount;
                LastMarble = lastMarble;
                scores = new long[playerCount];
                marbles = new LinkedList<int>();
                currentMarble = marbles.AddFirst(0);
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
                MoveBackward(7);
                scores[currentPlayer] += marble + currentMarble.Value;
                MoveForward(1);
                marbles.Remove(currentMarble.Previous ?? marbles.Last);
            }

            private void PlaceMarble(int marble)
            {
                MoveForward(1);
                currentMarble = marbles.AddAfter(currentMarble, marble);
            }

            private void MoveForward(int count)
            {
                for (var i = 0; i < count; ++i)
                    currentMarble = currentMarble.Next ?? marbles.First;
            }

            private void MoveBackward(int count)
            {
                for (var i = 0; i < count; ++i)
                    currentMarble = currentMarble.Previous ?? marbles.Last;
            }

            public long HighestScore()
            {
                return scores.Max();
            }
        }
    }
}