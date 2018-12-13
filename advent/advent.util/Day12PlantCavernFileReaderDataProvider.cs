using System.IO;
using System.Linq;
using advent.solvers;

namespace advent.util
{
    public class Day12PlantCavernFileReaderDataProvider : DataProvider<Day12Solver.PlantCavern>
    {
        private readonly string filename;

        public Day12PlantCavernFileReaderDataProvider(string filename)
        {
            this.filename = filename;
        }

        public Day12Solver.PlantCavern GetData()
        {
            var lines = File.ReadAllLines(filename);
            var state = lines.First().Substring(15).Replace('.', ' ');
            var notes = lines.Skip(2).Where(n => n.Substring(9, 1) == "#")
                .Select(n => n.Substring(0, 5).Replace('.', ' '));
            
            return new Day12Solver.PlantCavern(state, notes);
        }
    }
}