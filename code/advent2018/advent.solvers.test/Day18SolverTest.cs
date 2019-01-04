using System.Text;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day18SolverTest
    {
        private readonly Day18Solver.LumberMap map;
        private readonly Solver solver;

        public Day18SolverTest()
        {
            var acres = new[,]
            {
                {
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Lumberyard, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.Lumberyard, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees, Day18Solver.AcreType.Lumberyard,
                    Day18Solver.AcreType.OpenGround
                },
                {
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Lumberyard,
                    Day18Solver.AcreType.Trees, Day18Solver.AcreType.Lumberyard, Day18Solver.AcreType.Lumberyard,
                    Day18Solver.AcreType.Trees
                },
                {
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Lumberyard,
                    Day18Solver.AcreType.OpenGround
                },
                {
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees,
                    Day18Solver.AcreType.Lumberyard, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.Lumberyard
                },
                {
                    Day18Solver.AcreType.Lumberyard, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Lumberyard,
                    Day18Solver.AcreType.Trees, Day18Solver.AcreType.Trees, Day18Solver.AcreType.Trees,
                    Day18Solver.AcreType.Lumberyard, Day18Solver.AcreType.Trees, Day18Solver.AcreType.Lumberyard,
                    Day18Solver.AcreType.Trees
                },
                {
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.Lumberyard, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees,
                    Day18Solver.AcreType.Trees, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.OpenGround
                },
                {
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.Trees, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.OpenGround
                },
                {
                    Day18Solver.AcreType.Trees, Day18Solver.AcreType.Trees, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Lumberyard,
                    Day18Solver.AcreType.Trees, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Lumberyard,
                    Day18Solver.AcreType.Trees
                },
                {
                    Day18Solver.AcreType.Trees, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees,
                    Day18Solver.AcreType.Trees, Day18Solver.AcreType.Trees, Day18Solver.AcreType.Trees,
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees,
                    Day18Solver.AcreType.OpenGround
                },
                {
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround,
                    Day18Solver.AcreType.Lumberyard, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees,
                    Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.OpenGround, Day18Solver.AcreType.Trees,
                    Day18Solver.AcreType.OpenGround
                }
            };
            map = new Day18Solver.LumberMap(acres);

            var dataProvider = new Mock<DataProvider<Day18Solver.LumberMap>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(map);
            
            solver = new Day18Solver(dataProvider.Object);
        }

        [Fact]
        public void PrintsCorrectly()
        {
            var expectedMap = new StringBuilder()
                .AppendLine(".#.#...|#.")
                .AppendLine(".....#|##|")
                .AppendLine(".|..|...#.")
                .AppendLine("..|#.....#")
                .AppendLine("#.#|||#|#|")
                .AppendLine("...#.||...")
                .AppendLine(".|....|...")
                .AppendLine("||...#|.#|")
                .AppendLine("|.||||..|.")
                .AppendLine("...#.|..|.")
                .ToString();
            Assert.Equal(expectedMap, map.ToString());
        }

        [Fact]
        public void IteratesMap()
        {
            map.Iterate();
            var expectedMap = new StringBuilder()
                .AppendLine(".......##.")
                .AppendLine("......|###")
                .AppendLine(".|..|...#.")
                .AppendLine("..|#||...#")
                .AppendLine("..##||.|#|")
                .AppendLine("...#||||..")
                .AppendLine("||...|||..")
                .AppendLine("|||||.||.|")
                .AppendLine("||||||||||")
                .AppendLine("....||..|.")
                .ToString();
            Assert.Equal(expectedMap, map.ToString());
        }

        [Fact]
        public void IteratesTenTimes()
        {
            map.Iterate(10);
            var expectedMap = new StringBuilder()
                .AppendLine(".||##.....")
                .AppendLine("||###.....")
                .AppendLine("||##......")
                .AppendLine("|##.....##")
                .AppendLine("|##.....##")
                .AppendLine("|##....##|")
                .AppendLine("||##.####|")
                .AppendLine("||#####|||")
                .AppendLine("||||#|||||")
                .AppendLine("||||||||||")
                .ToString();
            Assert.Equal(expectedMap, map.ToString());
        }

        [Fact]
        public void ReturnsCountOfType()
        {
            map.Iterate(10);
            Assert.Equal(37, map.Count(Day18Solver.AcreType.Trees));
            Assert.Equal(31, map.Count(Day18Solver.AcreType.Lumberyard));
        }

        [Fact]
        public void ReturnsCountOfTreesMultipliedByCountOfLumberyards()
        {
            Assert.Equal("1147", solver.SolveFirstPart());
        }
    }
}