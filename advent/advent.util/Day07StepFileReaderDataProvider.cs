using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day07StepFileReaderDataProvider : DataProvider<IList<Day07Solver.Step>>
    {
        private readonly string filename;
        private readonly int minimumTime;

        private readonly Regex requirementParsingRegex;

        public Day07StepFileReaderDataProvider(string filename, int minimumTime = 60)
        {
            this.filename = filename;
            this.minimumTime = minimumTime;
            requirementParsingRegex = new Regex("^Step (\\w) must be finished before step (\\w) can begin.$");
        }

        public IList<Day07Solver.Step> GetData()
        {
            var requirementMatches = File.ReadAllLines(filename)
                .Select(line => requirementParsingRegex.Match(line))
                .ToList();
            var steps = requirementMatches.Select(match => match.Groups[1].Value[0])
                .Concat(requirementMatches.Select(match => match.Groups[2].Value[0]))
                .Distinct()
                .OrderBy(id => id)
                .Select(id => new Day07Solver.Step(id, minimumTime))
                .ToList();
            foreach (var match in requirementMatches)
                AddRequirement(steps, match.Groups[2].Value[0], match.Groups[1].Value[0]);

            return steps;
        }

        private static void AddRequirement(IList<Day07Solver.Step> steps, char step, char requires)
        {
            steps.Single(s => s.Id == step).Requirements.Add(steps.Single(s => s.Id == requires));
        }
    }
}