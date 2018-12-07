using System.Collections.Generic;
using System.Linq;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class StepFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsStepsFromFile()
        {
            const string filename = "data\\step_list.txt";
            const int minimumTime = 30;
            var steps = new List<Day07Solver.Step>
            {
                new Day07Solver.Step('A', minimumTime),
                new Day07Solver.Step('B', minimumTime),
                new Day07Solver.Step('C', minimumTime),
                new Day07Solver.Step('D', minimumTime),
                new Day07Solver.Step('E', minimumTime),
                new Day07Solver.Step('F', minimumTime)
            };
            AddRequirement(steps, 'A', 'C');
            AddRequirement(steps, 'F', 'C');
            AddRequirement(steps, 'B', 'A');
            AddRequirement(steps, 'D', 'A');
            AddRequirement(steps, 'E', 'B');
            AddRequirement(steps, 'E', 'D');
            AddRequirement(steps, 'E', 'F');
            Assert.Equal(steps, new StepFileReaderDataProvider(filename, minimumTime).GetData(), new StepComparer());
        }

        private static void AddRequirement(IList<Day07Solver.Step> steps, char step, char requires)
        {
            steps.Single(s => s.Id == step).Requirements.Add(steps.Single(s => s.Id == requires));
        }

        private class StepComparer : IEqualityComparer<Day07Solver.Step>
        {
            public bool Equals(Day07Solver.Step x, Day07Solver.Step y)
            {
                return x.Id == y.Id &&
                       x.IsCompleted == y.IsCompleted &&
                       x.RequiredTime == y.RequiredTime &&
                       x.Requirements.Select(s => s.Id).SequenceEqual(y.Requirements.Select(s => s.Id));
            }

            public int GetHashCode(Day07Solver.Step obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}