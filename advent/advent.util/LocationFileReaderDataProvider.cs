using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class LocationFileReaderDataProvider : DataProvider<Day06Solver.Location>
    {
        private readonly string filename;

        private readonly Regex coordinateParsingRegex;

        public LocationFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            
            coordinateParsingRegex = new Regex("^(\\d+), (\\d+)");
        }

        public IEnumerable<Day06Solver.Location> GetData()
        {
            return File.ReadAllLines(filename).Select(MakeCoordinate);
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