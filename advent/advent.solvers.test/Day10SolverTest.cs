using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day10SolverTest
    {
        private readonly Solver solver;

        public Day10SolverTest()
        {
            var dataProvider = new Mock<DataProvider<IList<Day10Solver.Light>>>();
            dataProvider.Setup(dp => dp.GetData())
                .Returns(new List<Day10Solver.Light>
                {
                    new Day10Solver.Light(9, 1, 0, 2),
                    new Day10Solver.Light(7, 0, -1, 0),
                    new Day10Solver.Light(3, -2, -1, 1),
                    new Day10Solver.Light(6, 10, -2, -1),
                    new Day10Solver.Light(2, -4, 2, 2),
                    new Day10Solver.Light(-6, 10, 2, -2),
                    new Day10Solver.Light(1, 8, 1, -1),
                    new Day10Solver.Light(1, 7, 1, 0),
                    new Day10Solver.Light(-3, 11, 1, -2),
                    new Day10Solver.Light(7, 6, -1, -1),
                    new Day10Solver.Light(-2, 3, 1, 0),
                    new Day10Solver.Light(-4, 3, 2, 0),
                    new Day10Solver.Light(10, -3, -1, 1),
                    new Day10Solver.Light(5, 11, 1, -2),
                    new Day10Solver.Light(4, 7, 0, -1),
                    new Day10Solver.Light(8, -2, 0, 1),
                    new Day10Solver.Light(15, 0, -2, 0),
                    new Day10Solver.Light(1, 6, 1, 0),
                    new Day10Solver.Light(8, 9, 0, -1),
                    new Day10Solver.Light(3, 3, -1, 1),
                    new Day10Solver.Light(0, 5, 0, -1),
                    new Day10Solver.Light(-2, 2, 2, 0),
                    new Day10Solver.Light(5, -2, 1, 2),
                    new Day10Solver.Light(1, 4, 2, 1),
                    new Day10Solver.Light(-2, 7, 2, -2),
                    new Day10Solver.Light(3, 6, -1, -1),
                    new Day10Solver.Light(5, 0, 1, 0),
                    new Day10Solver.Light(-6, 0, 2, 0),
                    new Day10Solver.Light(5, 9, 1, -2),
                    new Day10Solver.Light(14, 7, -2, 0),
                    new Day10Solver.Light(-3, 6, 2, -1)
                });

            solver = new Day10Solver(dataProvider.Object);
        }

        [Fact]
        public void ReturnsSmallestSky()
        {
            Assert.Equal(
                $"#   #  ###{Environment.NewLine}" +
                $"#   #   # {Environment.NewLine}" +
                $"#   #   # {Environment.NewLine}" +
                $"#####   # {Environment.NewLine}" +
                $"#   #   # {Environment.NewLine}" +
                $"#   #   # {Environment.NewLine}" +
                $"#   #   # {Environment.NewLine}" +
                $"#   #  ###{Environment.NewLine}",
                solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsTimeToSmalledSky()
        {
            Assert.Equal("3", solver.SolveSecondPart());
        }
    }
}