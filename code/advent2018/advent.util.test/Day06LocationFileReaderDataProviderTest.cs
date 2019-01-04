using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day06LocationFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsCoordinatesFromFile()
        {
            const string filename = "data\\day06_location_list.txt";
            var expected = new[]
            {
                new Day06Solver.Location(1, 1),
                new Day06Solver.Location(1, 6),
                new Day06Solver.Location(8, 3),
                new Day06Solver.Location(3, 4),
                new Day06Solver.Location(5, 5),
                new Day06Solver.Location(8, 9)
            };
            var coordinates = new Day06LocationFileReaderDataProvider(filename).GetData();
            Assert.Equal(expected, coordinates, new CoordinateComparer());
        }
    }

    public class CoordinateComparer : IEqualityComparer<Day06Solver.Location>
    {
        public bool Equals(Day06Solver.Location x, Day06Solver.Location y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(Day06Solver.Location obj)
        {
            throw new System.NotImplementedException();
        }
    }
}