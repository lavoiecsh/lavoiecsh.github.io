using System.Collections.Generic;
using System.IO;
using System.Linq;
using advent.solvers;

namespace advent.util
{
    public class NodeFileReaderDataProvider : DataProvider<Day08Solver.Node>
    {
        private readonly string filename;

        public NodeFileReaderDataProvider(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<Day08Solver.Node> GetData()
        {
            var license = File.ReadAllLines(filename).First().Split(' ').Select(int.Parse).ToList();
            return new List<Day08Solver.Node> {MakeNode(license)};
        }

        private static Day08Solver.Node MakeNode(IList<int> license)
        {
            var childCount = license.First();
            license.RemoveAt(0);
            var dataCount = license.First();
            license.RemoveAt(0);

            var children = new List<Day08Solver.Node>();
            for (var i = 0; i < childCount; ++i)
                children.Add(MakeNode(license));

            var metadata = new List<int>();
            for (var i = 0; i < dataCount; ++i)
            {
                metadata.Add(license.First());
                license.RemoveAt(0);
            }

            return new Day08Solver.Node(children, metadata);
        }
    }
}