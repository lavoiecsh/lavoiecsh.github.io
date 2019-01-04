using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day06LocationFileReaderDataProvider : DataProvider<IList<Day06Solver.Location>>
    {
        private readonly string filename;

        private readonly Regex coordinateParsingRegex;

        public Day06LocationFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            
            coordinateParsingRegex = new Regex("^(\\d+), (\\d+)");
        }

        public IList<Day06Solver.Location> GetData()
        {
            return File.ReadAllLines(filename).Select(MakeCoordinate).ToList();
        }

        private Day06Solver.Location MakeCoordinate(string line)
        {
            var match = coordinateParsingRegex.Match(line);
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            return new Day06Solver.Location(x, y);
        }
    }
}