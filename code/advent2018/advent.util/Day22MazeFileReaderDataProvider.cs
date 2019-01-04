using System.IO;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day22MazeFileReaderDataProvider : DataProvider<Day22Solver.Maze>
    {
        private readonly string filename;

        private readonly Regex depthParsingRegex;
        private readonly Regex targetParsingRegex;

        public Day22MazeFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            depthParsingRegex = new Regex("^depth: (\\d+)$");
            targetParsingRegex = new Regex("^target: (\\d+),(\\d+)");
        }

        public Day22Solver.Maze GetData()
        {
            var lines = File.ReadAllLines(filename);
            var depth = int.Parse(depthParsingRegex.Match(lines[0]).Groups[1].Value);
            var targetMatch = targetParsingRegex.Match(lines[1]);
            var targetX = int.Parse(targetMatch.Groups[1].Value);
            var targetY = int.Parse(targetMatch.Groups[2].Value);
            return new Day22Solver.Maze(depth, (targetX, targetY));
        }
    }
}