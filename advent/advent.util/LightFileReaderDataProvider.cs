using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class LightFileReaderDataProvider : DataProvider<Day10Solver.Light>
    {
        private readonly string filename;

        private readonly Regex lightParsingRegex;

        public LightFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            lightParsingRegex = new Regex("^position=<[ ]*([-]?\\d+),[ ]*([-]?\\d+)> velocity=<[ ]*([-]?\\d+),[ ]*([-]?\\d+)>$");
        }

        public IEnumerable<Day10Solver.Light> GetData()
        {
            return File.ReadAllLines(filename).Select(MakeLight);
        }

        private Day10Solver.Light MakeLight(string line)
        {
            var match = lightParsingRegex.Match(line);
            var posX = int.Parse(match.Groups[1].Value);
            var posY = int.Parse(match.Groups[2].Value);
            var velX = int.Parse(match.Groups[3].Value);
            var velY = int.Parse(match.Groups[4].Value);
            return new Day10Solver.Light(posX, posY, velX, velY);
        }
    }
}