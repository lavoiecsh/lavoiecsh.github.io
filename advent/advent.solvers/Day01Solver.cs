using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace advent.solvers
{
    public class Day01Solver : Solver
    {
        public string ProblemName => "ChronalCalibration";
        
        private readonly DataProvider<int> dataProvider;

        public Day01Solver(DataProvider<int> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var changes = dataProvider.GetData();
            return changes.Sum().ToString();
        }

        public string SolveSecondPart()
        {
            var changes = dataProvider.GetData();
            return FirstDuplicateFrequency(changes.ToList()).ToString();
        }
        
        private static int FirstDuplicateFrequency(IList<int> changes)
        {
            var frequency = 0;
            var frequencies = new Collection<int> {frequency};
            while (true)
            {
                foreach (var change in changes)
                {
                    frequency += change;
                    if (frequencies.Contains(frequency))
                        return frequency;
                    frequencies.Add(frequency);
                }
            }
        }
    }
}