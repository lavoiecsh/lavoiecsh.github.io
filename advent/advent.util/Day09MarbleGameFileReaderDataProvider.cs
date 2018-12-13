using System.IO;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day09MarbleGameFileReaderDataProvider : DataProvider<Day09Solver.MarbleGame>
    {
        private readonly string filename;

        private readonly Regex marbleGameParsingRegex;

        public Day09MarbleGameFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            marbleGameParsingRegex = new Regex("^(\\d+) players; last marble is worth (\\d+) points$");
        }

        public Day09Solver.MarbleGame GetData()
        {
            return MakeMarbleGame(File.ReadAllText(filename));
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