using System.Collections.Generic;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day25SolverTest
    {
        private readonly Solver solver;
        private readonly Mock<DataProvider<IList<Day25Solver.Point>>> dataProvider;

        public Day25SolverTest()
        {
            dataProvider = new Mock<DataProvider<IList<Day25Solver.Point>>>();
            solver = new Day25Solver(dataProvider.Object);
        }

        [Fact]
        public void ReturnsConstellationsForFirstSample()
        {
            var points = new List<Day25Solver.Point>
            {
                new Day25Solver.Point(0, 0, 0, 0),
                new Day25Solver.Point(3, 0, 0, 0),
                new Day25Solver.Point(0, 3, 0, 0),
                new Day25Solver.Point(0, 0, 3, 0),
                new Day25Solver.Point(0, 0, 0, 3),
                new Day25Solver.Point(0, 0, 0, 6),
                new Day25Solver.Point(9, 0, 0, 0),
                new Day25Solver.Point(12, 0, 0, 0)
            };
            dataProvider.Setup(dp => dp.GetData()).Returns(points);
            Assert.Equal("2", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsConstellationsForSecondSample()
        {
            var points = new List<Day25Solver.Point>
            {
                new Day25Solver.Point(-1, 2, 2, 0),
                new Day25Solver.Point(0, 0, 2, -2),
                new Day25Solver.Point(0, 0, 0, -2),
                new Day25Solver.Point(-1, 2, 0, 0),
                new Day25Solver.Point(-2, -2, -2, 2),
                new Day25Solver.Point(3, 0, 2, -1),
                new Day25Solver.Point(-1, 3, 2, 2),
                new Day25Solver.Point(-1, 0, -1, 0),
                new Day25Solver.Point(0, 2, 1, -2),
                new Day25Solver.Point(3, 0, 0, 0)
            };
            dataProvider.Setup(dp => dp.GetData()).Returns(points);
            Assert.Equal("4", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsConstellationsForThirdSample()
        {
            var points = new List<Day25Solver.Point>
            {
                new Day25Solver.Point(1, -1, 0, 1),
                new Day25Solver.Point(2, 0, -1, 0),
                new Day25Solver.Point(3, 2, -1, 0),
                new Day25Solver.Point(0, 0, 3, 1),
                new Day25Solver.Point(0, 0, -1, -1),
                new Day25Solver.Point(2, 3, -2, 0),
                new Day25Solver.Point(-2, 2, 0, 0),
                new Day25Solver.Point(2, -2, 0, -1),
                new Day25Solver.Point(1, -1, 0, -1),
                new Day25Solver.Point(3, 2, 0, 2)
            };
            dataProvider.Setup(dp => dp.GetData()).Returns(points);
            Assert.Equal("3", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsConstellationsForFourthSample()
        {
            var points = new List<Day25Solver.Point>
            {
                new Day25Solver.Point(1, -1, -1, -2),
                new Day25Solver.Point(-2, -2, 0, 1),
                new Day25Solver.Point(0, 2, 1, 3),
                new Day25Solver.Point(-2, 3, -2, 1),
                new Day25Solver.Point(0, 2, 3, -2),
                new Day25Solver.Point(-1, -1, 1, -2),
                new Day25Solver.Point(0, -2, -1, 0),
                new Day25Solver.Point(-2, 2, 3, -1),
                new Day25Solver.Point(1, 2, 2, 0),
                new Day25Solver.Point(-1, -2, 0, -2)
            };
            dataProvider.Setup(dp => dp.GetData()).Returns(points);
            Assert.Equal("8", solver.SolveFirstPart());
        }
    }
}