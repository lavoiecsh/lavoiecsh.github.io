using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day17ClayFileReaderDataProvider : DataProvider<Day17Solver.WaterMap>
    {
        private readonly string filename;

        private readonly Regex verticalParsingRegex;
        private readonly Regex horizontalParsingRegex;

        public Day17ClayFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            verticalParsingRegex = new Regex("^x=(\\d+), y=(\\d+)\\.\\.(\\d+)$");
            horizontalParsingRegex = new Regex("^y=(\\d+), x=(\\d+)\\.\\.(\\d+)$");
        }

        public Day17Solver.WaterMap GetData()
        {
            var lines = File.ReadAllLines(filename);
            var clayPatches = new List<(int X, int Y)>();
            foreach (var line in lines)
            {
                var verticalMatch = verticalParsingRegex.Match(line);
                if (verticalMatch.Success)
                {
                    var x = int.Parse(verticalMatch.Groups[1].Value);
                    var yStart = int.Parse(verticalMatch.Groups[2].Value);
                    var yEnd = int.Parse(verticalMatch.Groups[3].Value);
                    for (var y = yStart; y <= yEnd; ++y)
                        clayPatches.Add((x, y));
                    continue;
                }
                
                var horizontalMatch = horizontalParsingRegex.Match(line);
                if (horizontalMatch.Success)
                {
                    var y = int.Parse(horizontalMatch.Groups[1].Value);
                    var xStart = int.Parse(horizontalMatch.Groups[2].Value);
                    var xEnd = int.Parse(horizontalMatch.Groups[3].Value);
                    for (var x = xStart; x <= xEnd; ++x)
                        clayPatches.Add((x, y));
                }
            }

            return new Day17Solver.WaterMap(clayPatches);
        }
    }
}