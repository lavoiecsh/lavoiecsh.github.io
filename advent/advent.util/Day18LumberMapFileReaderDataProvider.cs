using System.IO;
using advent.solvers;

namespace advent.util
{
    public class Day18LumberMapFileReaderDataProvider : DataProvider<Day18Solver.LumberMap>
    {
        private readonly string filename;

        public Day18LumberMapFileReaderDataProvider(string filename)
        {
            this.filename = filename;
        }

        public Day18Solver.LumberMap GetData()
        {
            var lines = File.ReadAllLines(filename);
            var acres = new Day18Solver.AcreType[lines.Length, lines[0].Length];
            for (var i = 0; i < lines.Length; ++i)
            for (var j = 0; j < lines[i].Length; ++j)
                acres[i, j] = (Day18Solver.AcreType) lines[i][j];
            return new Day18Solver.LumberMap(acres);
        }
    }
}