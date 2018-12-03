using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using advent.util;

namespace advent.solvers
{
    public class Day1Solver : ISolver
    {
        public string ProblemName => "ChronalCalibration";
        
        private readonly IFileReader fileReader;

        public Day1Solver(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public string Solve1(IEnumerable<string> args)
        {
            var changes = fileReader.ReadInts(args.First());
            return changes.Sum().ToString();
        }

        public string Solve2(IEnumerable<string> args)
        {
            var changes = fileReader.ReadInts(args.First());
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