using System;
using System.Text;

namespace advent.solvers
{
    public class Day14Solver : Solver
    {
        public string ProblemName => "Chocolate Charts";

        private readonly string input;

        public Day14Solver(string input)
        {
            this.input = input;
        }

        public string SolveFirstPart()
        {
            var recipes = new Recipes();
            var count = int.Parse(input);
            while (recipes.Scores.Length <= count + 10)
                recipes.Iterate();
            return recipes.Scores.ToString().Substring(count, 10);
        }

        public string SolveSecondPart()
        {
            var recipes = new Recipes(input.Length + 1);
            while (!recipes.Contains(input))
                recipes.Iterate();
            return recipes.Scores.ToString().IndexOf(input, StringComparison.Ordinal).ToString();
        }

        public class Recipes
        {
            public readonly StringBuilder Scores;
            public int FirstElfIndex { get; private set; }
            public int SecondElfIndex { get; private set; }
            private string lastScores;
            private readonly int keepLast;

            public Recipes() : this(0)
            {
            }

            public Recipes(int keepLast)
            {
                Scores = new StringBuilder("37");
                FirstElfIndex = 0;
                SecondElfIndex = 1;
                this.keepLast = keepLast;
                lastScores = "37";
            }

            public void Iterate()
            {
                CalculateNextRecipes();
                MoveElves();
            }

            public void CalculateNextRecipes()
            {
                var sum = Scores[FirstElfIndex] - 48 + Scores[SecondElfIndex] - 48;
                if (sum >= 10)
                    Add(sum / 10);
                Add(sum % 10);
            }

            private void Add(int score)
            {
                Scores.Append(score.ToString());
                if (keepLast == 0) return;
                lastScores += score.ToString();
                if (lastScores.Length > keepLast)
                    lastScores = lastScores.Remove(0, lastScores.Length - keepLast);
            }

            public bool Contains(string input)
            {
                return lastScores.Contains(input);
            }

            public void MoveElves()
            {
                FirstElfIndex += Scores[FirstElfIndex] - 48 + 1;
                while (FirstElfIndex >= Scores.Length)
                    FirstElfIndex -= Scores.Length;
                SecondElfIndex += Scores[SecondElfIndex] - 48 + 1;
                while (SecondElfIndex >= Scores.Length)
                    SecondElfIndex -= Scores.Length;
            }
        }
    }
}