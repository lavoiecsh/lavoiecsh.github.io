using Xunit;

namespace advent.solvers.test
{
    public class Day11SolverTest
    {
        [Theory]
        [InlineData(8, 3, 5, 4)]
        [InlineData(57, 122, 79, -5)]
        [InlineData(39, 217, 196, 0)]
        [InlineData(71, 101, 153, 4)]
        public void ReturnsPowerLevelOfFuelCell(int serialNumber, int x, int y, int expectedPowerLevel)
        {
            Assert.Equal(expectedPowerLevel, Day11Solver.Cell.ComputePowerLevel(serialNumber, x, y));
        }

        [Theory]
        [InlineData(18, 33, 45, 29)]
        [InlineData(42, 21, 61, 30)]
        public void ReturnsPowerLevelOfGrid(int serialNumber, int x, int y, int expectedPowerLevel)
        {
            var powerGrid = new Day11Solver.PowerGrid(serialNumber);
            var cellGrid = powerGrid.GetCellGrid(x, y, 3);
            Assert.Equal(expectedPowerLevel, cellGrid.PowerLevel);
        }

        [Theory]
        [InlineData(18, 33, 45, 29)]
        [InlineData(42, 21, 61, 30)]
        public void ReturnsGridWithHighestPowerLevel(int serialNumber,
            int expectedX,
            int expectedY,
            int expectedPowerLevel)
        {
            var powerGrid = new Day11Solver.PowerGrid(serialNumber);
            var highestCellGrid = powerGrid.FindHighestLevelCellGrid(3);
            Assert.Equal(expectedX, highestCellGrid.X);
            Assert.Equal(expectedY, highestCellGrid.Y);
            Assert.Equal(3, highestCellGrid.Size);
            Assert.Equal(expectedPowerLevel, highestCellGrid.PowerLevel);
        }

        [Theory]
        [InlineData(18, "33,45")]
        [InlineData(42, "21,61")]
        public void ReturnsTopLeftOfHighestPowerGrid(int serialNumber, string expectedTopLeft)
        {
            var solver = new Day11Solver(serialNumber);
            Assert.Equal(expectedTopLeft, solver.SolveFirstPart());
        }

        [Theory]
        [InlineData(18, 90, 269, 16, 113)]
        [InlineData(42, 232, 251, 12, 119)]
        public void ReturnsVariableGridWithHighestPowerLevel(int serialNumber,
            int expectedX,
            int expectedY,
            int expectedSize,
            int expectedPowerLevel)
        {
            var powerGrid = new Day11Solver.PowerGrid(serialNumber);
            var splitPowerGrid = new Day11Solver.SplitPowerGrid(powerGrid);
            var cellGrid = splitPowerGrid.FindHighestLevelCellGrid();
            Assert.Equal(expectedX, cellGrid.X);
            Assert.Equal(expectedY, cellGrid.Y);
            Assert.Equal(expectedSize, cellGrid.Size);
            Assert.Equal(expectedPowerLevel, cellGrid.PowerLevel);
        }

        [Theory]
        [InlineData(18, "90,269,16")]
        [InlineData(42, "232,251,12")]
        public void ReturnsTopLeftAndSizeOfHighestPowerGrid(int serialNumber, string expectedTopLeftSize)
        {
            var solver = new Day11Solver(serialNumber);
            Assert.Equal(expectedTopLeftSize, solver.SolveSecondPart());
        }
    }
}