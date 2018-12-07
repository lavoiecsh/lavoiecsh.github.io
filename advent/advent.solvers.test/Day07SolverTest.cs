using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day07SolverTest
    {
        private readonly Solver solver;

        public Day07SolverTest()
        {
            const int minimumTime = 0;
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
            var dataProvider = new Mock<DataProvider<Day07Solver.Step>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(steps);
            
            solver = new Day07Solver(dataProvider.Object, 2);
        }

        private static void AddRequirement(IList<Day07Solver.Step> steps, char step, char requires)
        {
            steps.Single(s => s.Id == step).Requirements.Add(steps.Single(s => s.Id == requires));
        }

        [Fact]
        public void ReturnsOrderToCompleteStepIn()
        {
            Assert.Equal("CABDFE", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsTimeRequiredWithMultipleWorkers()
        {
            Assert.Equal("15", solver.SolveSecondPart());
        }
    }
}