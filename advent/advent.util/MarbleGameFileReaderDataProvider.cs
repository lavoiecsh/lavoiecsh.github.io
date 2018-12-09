using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class MarbleGameFileReaderDataProvider : DataProvider<Day09Solver.MarbleGame>
    {
        private readonly string filename;

        private readonly Regex marbleGameParsingRegex;

        public MarbleGameFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            marbleGameParsingRegex = new Regex("^(\\d+) players; last marble is worth (\\d+) points$");
        }

        public IEnumerable<Day09Solver.MarbleGame> GetData()
        {
            return File.ReadAllLines(filename).Select(MakeMarbleGame);
        }

        private Day09Solver.MarbleGame MakeMarbleGame(string line)
        {
            var match = marbleGameParsingRegex.Match(line);
            var playerCount = int.Parse(match.Groups[1].Value);
            var lastMarble = int.Parse(match.Groups[2].Value);
            return new Day09Solver.MarbleGame(playerCount, lastMarble);
        }
    }
}