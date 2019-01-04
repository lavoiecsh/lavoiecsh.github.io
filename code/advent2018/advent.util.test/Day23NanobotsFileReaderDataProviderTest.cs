using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day23NanobotsFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsNanobotsInFile()
        {
            const string filename = "data\\day23_nanobot_list.txt";
            var expectedNanobots = new List<Day23Solver.Nanobot>
            {
                new Day23Solver.Nanobot(0, 0, 0, 4),
                new Day23Solver.Nanobot(1, 0, 0, 1),
                new Day23Solver.Nanobot(4, 0, 0, 3),
                new Day23Solver.Nanobot(0, 2, 0, 1),
                new Day23Solver.Nanobot(0, 5, 0, 3),
                new Day23Solver.Nanobot(0, 0, 3, 1),
                new Day23Solver.Nanobot(1, 1, 1, 1),
                new Day23Solver.Nanobot(1, 1, 2, 1),
                new Day23Solver.Nanobot(1, 3, 1, 1)
            };
            var nanobots = new Day23NanobotsFileReaderDataProvider(filename).GetData();
            Assert.Equal(expectedNanobots, nanobots, new NanobotComparer());
        }
        
        private class NanobotComparer : IEqualityComparer<Day23Solver.Nanobot>
        {
            public bool Equals(Day23Solver.Nanobot x, Day23Solver.Nanobot y)
            {
                return x.Position.X == y.Position.X &&
                       x.Position.Y == y.Position.Y &&
                       x.Position.Z == y.Position.Z &&
                       x.Radius == y.Radius;
            }

            public int GetHashCode(Day23Solver.Nanobot obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}