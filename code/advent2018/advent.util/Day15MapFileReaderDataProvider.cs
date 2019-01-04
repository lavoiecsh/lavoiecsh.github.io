using System.Collections.Generic;
using System.IO;
using advent.solvers;

namespace advent.util
{
    public class Day15MapFileReaderDataProvider : DataProvider<Day15Solver.Map>
    {
        private readonly string filename;

        public Day15MapFileReaderDataProvider(string filename)
        {
            this.filename = filename;
        }

        public Day15Solver.Map GetData()
        {
            var lines = File.ReadAllLines(filename);
            var openTiles = new List<Day15Solver.Position>();
            var units = new List<Day15Solver.Unit>();
            for (var i = 0; i < lines.Length; ++i)
            {
                for (var j = 0; j < lines[i].Length; ++j)
                {
                    switch (lines[i][j])
                    {
                        case '.':
                            openTiles.Add((j, i));
                            break;
                        case 'G':
                            units.Add(new Day15Solver.Unit((j, i), Day15Solver.Unit.UnitType.Goblin));
                            goto case '.';
                        case 'E':
                            units.Add(new Day15Solver.Unit((j, i), Day15Solver.Unit.UnitType.Elf));
                            goto case '.';
                    }
                }
            }
            return new Day15Solver.Map(openTiles, units);
        }
    }
}