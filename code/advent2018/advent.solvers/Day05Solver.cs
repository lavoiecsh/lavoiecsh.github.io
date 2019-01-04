using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day05Solver : Solver
    {
        public string ProblemName => "Alchemical Reduction";

        private readonly DataProvider<string> dataProvider;

        public Day05Solver(DataProvider<string> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var polymer = dataProvider.GetData();
            var reactedPolymer = React(polymer);
            return reactedPolymer.Length.ToString();
        }

        public string SolveSecondPart()
        {
            var polymer = dataProvider.GetData();
            IList<string> improvedPolymers = new List<string>();
            foreach (var problemUnit in "abcdefghijklmnopqrstuvwxyz")
            {
                var polarizedProblemUnit = (char) (problemUnit - 32);
                var improvedPolymer = polymer.Replace(problemUnit.ToString(), string.Empty)
                    .Replace(polarizedProblemUnit.ToString(), string.Empty);
                improvedPolymers.Add(React(improvedPolymer));
            }

            return improvedPolymers.Min(ip => ip.Length).ToString();
        }

        private static string React(string polymer)
        {
            int length;
            do
            {
                length = polymer.Length;
                polymer = RemoveInteractions(polymer);
            }
            while (polymer.Length != length);

            return polymer;
        }

        private static string RemoveInteractions(string polymer)
        {
            for (var i = polymer.Length - 2; i >= 0; --i)
            {
                if (!Interacts(polymer[i], polymer[i + 1])) continue;
                polymer = polymer.Remove(i, 2);
                --i;
            }

            return polymer;
        }

        private static bool Interacts(char firstUnit, char secondUnit)
        {
            return Math.Abs(firstUnit - secondUnit) == 32;
        }
    }
}