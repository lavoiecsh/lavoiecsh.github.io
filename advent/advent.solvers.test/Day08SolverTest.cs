using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day08SolverTest
    {
        private readonly Solver solver;

        public Day08SolverTest()
        {
            var nodeD = new Day08Solver.Node(new Day08Solver.Node[] { }, new[] {99});
            var nodeC = new Day08Solver.Node(new[] {nodeD}, new[] {2});
            var nodeB = new Day08Solver.Node(new Day08Solver.Node[] { }, new[] {10, 11, 12});
            var nodeA = new Day08Solver.Node(new[] {nodeB, nodeC}, new[] {1, 1, 2});

            var dataProvider = new Mock<DataProvider<Day08Solver.Node>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(new[] {nodeA});
            solver = new Day08Solver(dataProvider.Object);
        }

        [Fact]
        public void ReturnsSumOfAllMetadata()
        {
            Assert.Equal("138", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsValueOfNodes()
        {
            Assert.Equal("66", solver.SolveSecondPart());
        }

        [Fact]
        public void ReturnsValueForChildlessNode()
        {
            var node = new Day08Solver.Node(new Day08Solver.Node[] { }, new[] {1, 2, 3});
            Assert.Equal(6, node.Value());
        }

        [Fact]
        public void ReturnsValueForParentNode()
        {
            var node1 = new Day08Solver.Node(new Day08Solver.Node[] { }, new[] {1, 2, 3});
            var node = new Day08Solver.Node(new[] {node1}, new[] {1});
            Assert.Equal(6, node.Value());
        }
    }
}