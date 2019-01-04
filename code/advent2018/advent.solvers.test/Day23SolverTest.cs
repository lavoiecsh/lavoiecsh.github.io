using System.Collections.Generic;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day23SolverTest
    {
        private readonly IList<Day23Solver.Nanobot> sample1;
        private readonly IList<Day23Solver.Nanobot> sample2;

        private readonly Solver solver;
        private readonly Mock<DataProvider<IList<Day23Solver.Nanobot>>> dataProvider;

        public Day23SolverTest()
        {
            sample1 = new List<Day23Solver.Nanobot>
            {
                new Day23Solver.Nanobot(0, 0, 0, 4),
                new Day23Solver.Nanobot(1, 0, 0, 1),
                new Day23Solver.Nanobot(4, 0, 0, 3),
                new Day23Solver.Nanobot(0, 2, 0, 1),
                new Day23Solver.Nanobot(0, 5, 0, 3),
                new Day23Solver.Nanobot(0, 0, 3, 1),
                new Day23Solver.Nanobot(1, 1, 1, 1),
                new Day23Solver.Nanobot(1, 1, 2, 1),
                new Day23Solver.Nanobot(1, 3, 1, 1)
            };
            sample2 = new List<Day23Solver.Nanobot>
            {
                new Day23Solver.Nanobot(10, 12, 12, 2),
                new Day23Solver.Nanobot(12, 14, 12, 2),
                new Day23Solver.Nanobot(16, 12, 12, 4),
                new Day23Solver.Nanobot(14, 14, 14, 6),
                new Day23Solver.Nanobot(50, 50, 50, 200),
                new Day23Solver.Nanobot(10, 10, 10, 5)
            };

            dataProvider = new Mock<DataProvider<IList<Day23Solver.Nanobot>>>();
            solver = new Day23Solver(dataProvider.Object);
        }

        [Fact]
        public void ReturnsNumberOfNanobotsInRangeOfLargestRadiusNanobot()
        {
            dataProvider.Setup(dp => dp.GetData()).Returns(sample1);
            Assert.Equal("7", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsDistanceFromOriginToMostCoveredPosition()
        {
            dataProvider.Setup(dp => dp.GetData()).Returns(sample2);
            Assert.Equal("36", solver.SolveSecondPart());
        }
    }
}