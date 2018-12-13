using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day08NodeFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsNodeFromFile()
        {
            const string filename = "data\\day08_node_list.txt";
            var nodeD = new Day08Solver.Node(new Day08Solver.Node[] { }, new[] {99});
            var nodeC = new Day08Solver.Node(new[] {nodeD}, new[] {2});
            var nodeB = new Day08Solver.Node(new Day08Solver.Node[] { }, new[] {10, 11, 12});
            var nodeA = new Day08Solver.Node(new[] {nodeB, nodeC}, new[] {1, 1, 2});

            Assert.Equal(nodeA, new Day08NodeFileReaderDataProvider(filename).GetData(), new NodeComparer());
        }

        private class NodeComparer : IEqualityComparer<Day08Solver.Node>
        {
            public bool Equals(Day08Solver.Node x, Day08Solver.Node y)
            {
                return x.MetadataSum() == y.MetadataSum();
            }

            public int GetHashCode(Day08Solver.Node obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}