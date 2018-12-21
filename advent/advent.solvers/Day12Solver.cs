using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace advent.solvers
{
    public class Day12Solver : Solver
    {
        public string ProblemName => "Subterranean Sustainability";

        private readonly DataProvider<PlantCavern> dataProvider;

        public Day12Solver(DataProvider<PlantCavern> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var cavern = dataProvider.GetData();
            for (var i = 0; i < 20; ++i)
                cavern.NextGeneration();
            return cavern.Sum().ToString();
        }

        public string SolveSecondPart()
        {
            var cavern = dataProvider.GetData();
            var watch = new Stopwatch();
            watch.Start();
            var lastState = cavern.State;
            var lastSum = cavern.Sum();
            long timesRemaining = -1;
            var sumDifference = -1;
            const long times = 50000000000;
            for (long i = 1; i <= 50000000000; ++i)
            {
                cavern.NextGeneration();
                if (cavern.State == lastState)
                {
                    sumDifference = cavern.Sum() - lastSum;
                    timesRemaining = times - i + 1;
                    break;
                }

                lastState = cavern.State;
                lastSum = cavern.Sum();
            }

            var sum = lastSum + timesRemaining * sumDifference;
            return sum.ToString();
        }

        public class PlantCavern
        {
            public string State { get; private set; }
            public IDictionary<string, char> Notes { get; }
            public int StartIndex { get; private set; }

            public PlantCavern(string state, IEnumerable<string> notes)
            {
                State = state;
                var notesList = notes.ToList();
                StartIndex = 0;
                Notes = new List<string>
                {
                    "     ", "    #", "   # ", "   ##", "  #  ", "  # #", "  ## ", "  ###", " #   ", " #  #", " # # ",
                    " # ##", " ##  ", " ## #", " ### ", " ####", "#    ", "#   #", "#  # ", "#  ##", "# #  ", "# # #",
                    "# ## ", "# ###", "##   ", "##  #", "## # ", "## ##", "###  ", "### #", "#### ", "#####"
                }.ToDictionary(n => n, n => notesList.Contains(n) ? '#' : ' ');
            }

            public void NextGeneration()
            {
                var currentState = $"    {State}    ";
                var nextState = new char[currentState.Length - 4];
                for (var i = 2; i < currentState.Length - 2; ++i)
                    nextState[i - 2] = Notes[currentState.Substring(i - 2, 5)];
                State = new string(nextState);
                StartIndex += State.IndexOf('#') - 2;
                State = State.Trim();
            }

            public int Sum()
            {
                return State.Zip(Enumerable.Range(StartIndex, State.Length), (p, i) => p == '#' ? i : 0).Sum();
            }
        }
    }
}