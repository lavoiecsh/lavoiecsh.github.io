using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day17SolverTest
    {
        private readonly Solver solver;
        private readonly Day17Solver.WaterMap waterMap;

        public Day17SolverTest()
        {
            var clayPatches = new List<(int X, int Y)>
            {
                (495, 2), (495, 3), (495, 4), (495, 5), (495, 6), (495, 7),
                (495, 7), (496, 7), (497, 7), (498, 7), (499, 7), (500, 7), (501, 7),
                (501, 3), (501, 4), (501, 5), (501, 6), (501, 7),
                (498, 2), (498, 3), (498, 4),
                (506, 1), (506, 2),
                (498, 10), (498, 11), (498, 12), (498, 13),
                (504, 10), (504, 11), (504, 12), (504, 13),
                (498, 13), (499, 13), (500, 13), (501, 13), (502, 13), (503, 13), (504, 13)
            };
            waterMap = new Day17Solver.WaterMap(clayPatches);

            var dataProvider = new Mock<DataProvider<Day17Solver.WaterMap>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(waterMap);
            
            solver = new Day17Solver(dataProvider.Object);
        }

        [Fact]
        public void RendersMapCorrectly()
        {
            var expectedMap = new StringBuilder()
                .AppendLine("......+.......")
                .AppendLine("............#.")
                .AppendLine(".#..#.......#.")
                .AppendLine(".#..#..#......")
                .AppendLine(".#..#..#......")
                .AppendLine(".#.....#......")
                .AppendLine(".#.....#......")
                .AppendLine(".#######......")
                .AppendLine("..............")
                .AppendLine("..............")
                .AppendLine("....#.....#...")
                .AppendLine("....#.....#...")
                .AppendLine("....#.....#...")
                .AppendLine("....#######...");
            Assert.Equal(expectedMap.ToString(), waterMap.ToString());
        }

        [Fact]
        public void CalculatesNextWaterStep()
        {
            waterMap.CalculateWaterFlow(500, 0);
            var expectedMap = new StringBuilder()
                .AppendLine("......+.......")
                .AppendLine("......|.....#.")
                .AppendLine(".#..#.......#.")
                .AppendLine(".#..#..#......")
                .AppendLine(".#..#..#......")
                .AppendLine(".#.....#......")
                .AppendLine(".#.....#......")
                .AppendLine(".#######......")
                .AppendLine("..............")
                .AppendLine("..............")
                .AppendLine("....#.....#...")
                .AppendLine("....#.....#...")
                .AppendLine("....#.....#...")
                .AppendLine("....#######...");
            Assert.Equal(expectedMap.ToString(), waterMap.ToString());
        }

        [Fact]
        public void FillsBucket()
        {
            for (var i = 0; i < 7; ++i)
                waterMap.CalculateWaterFlow(500, i);
            var expectedMap = new StringBuilder()
                .AppendLine("......+.......")
                .AppendLine("......|.....#.")
                .AppendLine(".#..#||||...#.")
                .AppendLine(".#..#~~#......")
                .AppendLine(".#..#~~#......")
                .AppendLine(".#~~~~~#......")
                .AppendLine(".#~~~~~#......")
                .AppendLine(".#######......")
                .AppendLine("..............")
                .AppendLine("..............")
                .AppendLine("....#.....#...")
                .AppendLine("....#.....#...")
                .AppendLine("....#.....#...")
                .AppendLine("....#######...");
            Assert.Equal(expectedMap.ToString(), waterMap.ToString());
        }

        [Fact]
        public void CalculatesAllWaterFlow()
        {
            waterMap.CalculateWaterFlow();
            var expectedMap = new StringBuilder()
                .AppendLine("......+.......")
                .AppendLine("......|.....#.")
                .AppendLine(".#..#||||...#.")
                .AppendLine(".#..#~~#|.....")
                .AppendLine(".#..#~~#|.....")
                .AppendLine(".#~~~~~#|.....")
                .AppendLine(".#~~~~~#|.....")
                .AppendLine(".#######|.....")
                .AppendLine("........|.....")
                .AppendLine("...|||||||||..")
                .AppendLine("...|#~~~~~#|..")
                .AppendLine("...|#~~~~~#|..")
                .AppendLine("...|#~~~~~#|..")
                .AppendLine("...|#######|..");
            Assert.Equal(expectedMap.ToString(), waterMap.ToString());
        }

        [Fact]
        public void ReturnsNumberOfWateredSquares()
        {
            Assert.Equal("57", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsNumberOfUndrainedTiles()
        {
            Assert.Equal("29", solver.SolveSecondPart());
        }
    }
}