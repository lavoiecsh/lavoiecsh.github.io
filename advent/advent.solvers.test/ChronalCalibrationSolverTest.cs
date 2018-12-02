using advent.util;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class ChronalCalibrationSolverTest
    {
        private readonly ChronalCalibrationSolver chronalCalibration;

        private readonly Mock<IFileReader> fileReader;

        public ChronalCalibrationSolverTest()
        {
            fileReader = new Mock<IFileReader>();
            chronalCalibration = new ChronalCalibrationSolver(fileReader.Object);
        }

        [Theory]
        [InlineData(3, 1, 1, 1)]
        [InlineData(0, 1, 1, -2)]
        [InlineData(-6, -1, -2, -3)]
        public void ReturnsFrequency(int expected, params int[] changes)
        {
            const string filename = "file.txt";
            fileReader.Setup(fr => fr.ReadInts(filename)).Returns(changes);
            Assert.Equal(expected, chronalCalibration.Solve1(new[] {filename}));
        }

        [Theory]
        [InlineData(0, 1, -1)]
        [InlineData(10, 3, 3, 4, -2, -4)]
        [InlineData(5, -6, 3, 8, 5, -6)]
        [InlineData(14, 7, 7, -2, -7, -4)]
        public void ReturnsFirstDuplicateFrequency(int expected, params int[] changes)
        {
            const string filename = "file.txt";
            fileReader.Setup(fr => fr.ReadInts(filename)).Returns(changes);
            Assert.Equal(expected, chronalCalibration.Solve2(new[] {filename}));
        }
    }
}