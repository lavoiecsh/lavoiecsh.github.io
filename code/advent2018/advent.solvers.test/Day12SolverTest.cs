using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day12SolverTest
    {
        private readonly Solver solver;

        private readonly Day12Solver.PlantCavern sample;

        public Day12SolverTest()
        {
            sample = new Day12Solver.PlantCavern("#  # #  ##      ###   ###",
                new[]
                {
                    "   ##",
                    "  #  ",
                    " #   ",
                    " # # ",
                    " # ##",
                    " ##  ",
                    " ####",
                    "# # #",
                    "# ###",
                    "## # ",
                    "## ##",
                    "###  ",
                    "### #",
                    "#### "
                });
            var dataProvider = new Mock<DataProvider<Day12Solver.PlantCavern>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(sample);

            solver = new Day12Solver(dataProvider.Object);
        }

        [Fact]
        public void CalculatesNextGeneration()
        {
            sample.NextGeneration();
            Assert.Equal("#   #    #     #  #  #  #", sample.State);
            Assert.Equal(0, sample.StartIndex);

            sample.NextGeneration();
            Assert.Equal("##  ##   ##    #  #  #  ##", sample.State);
            Assert.Equal(0, sample.StartIndex);

            sample.NextGeneration();
            Assert.Equal("# #   #  # #    #  #  #   #", sample.State);
            Assert.Equal(-1, sample.StartIndex);
        }

        [Fact]
        public void ReturnsSumOfCurrentState()
        {
            Assert.Equal(145, sample.Sum());
            for (var i = 0; i < 20; ++i)
                sample.NextGeneration();
            Assert.Equal(325, sample.Sum());
        }

        [Fact]
        public void ReturnsSumOfStateAfter20Generations()
        {
            Assert.Equal("325", solver.SolveFirstPart());
        }
    }
}