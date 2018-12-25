using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day25PointFileReaderDataProvider : DataProvider<IList<Day25Solver.Point>>
    {
        private readonly string filename;

        private readonly Regex pointParsingRegex;

        public Day25PointFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            pointParsingRegex = new Regex("^(-?\\d+),(-?\\d+),(-?\\d+),(-?\\d+)$");
        }

        public IList<Day25Solver.Point> GetData()
        {
            return File.ReadAllLines(filename).Select(MakePoint).ToList();
        }

        private Day25Solver.Point MakePoint(string line)
        {
            var match = pointParsingRegex.Match(line);
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            var z = int.Parse(match.Groups[3].Value);
            var w = int.Parse(match.Groups[4].Value);
            return new Day25Solver.Point(x, y, z, w);
        }
    }
}