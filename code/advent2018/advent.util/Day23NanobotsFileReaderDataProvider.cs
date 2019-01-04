using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day23NanobotsFileReaderDataProvider : DataProvider<IList<Day23Solver.Nanobot>>
    {
        private readonly string filename;
        private readonly Regex nanobotParsingRegex;

        public Day23NanobotsFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            nanobotParsingRegex = new Regex("^pos=<(-?\\d+),(-?\\d+),(-?\\d+)>, r=(\\d+)$");
        }

        public IList<Day23Solver.Nanobot> GetData()
        {
            return File.ReadAllLines(filename).Select(MakeNanobot).ToList();
        }

        private Day23Solver.Nanobot MakeNanobot(string line)
        {
            var match = nanobotParsingRegex.Match(line);
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            var z = int.Parse(match.Groups[3].Value);
            var r = int.Parse(match.Groups[4].Value);
            return new Day23Solver.Nanobot(x, y, z, r);
        }
    }
}