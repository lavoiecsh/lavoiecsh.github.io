using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day08Solver : Solver
    {
        public string ProblemName => "Memory Maneuver";
        
        private readonly DataProvider<Node> dataProvider;

        public Day08Solver(DataProvider<Node> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            return dataProvider.GetData().First().MetadataSum().ToString();
        }

        public string SolveSecondPart()
        {
            return dataProvider.GetData().First().Value().ToString();
        }

        public class Node
        {
            private readonly IList<Node> children;
            private readonly IList<int> metadata;

            public Node(IEnumerable<Node> children, IEnumerable<int> metadata)
            {
                this.children = children.ToList();
                this.metadata = metadata.ToList();
            }

            public int MetadataSum()
            {
                return children.Aggregate(metadata.Sum(), (acc, cur) => acc + cur.MetadataSum());
            }

            public int Value()
            {
                return children.Any() ? metadata.Aggregate(0, (acc, md) => acc + ChildValue(md)) : metadata.Sum();
            }

            private int ChildValue(int childIndex)
            {
                return childIndex > children.Count ? 0 : children[childIndex-1].Value();
            }
        }
    }
}