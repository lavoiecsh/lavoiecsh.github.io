using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day25PointFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsPointsInFile()
        {
            const string filename = "data\\day25_point_list.txt";
            var expectedPoints = new List<Day25Solver.Point>
            {
                new Day25Solver.Point(-1, 2, 2, 0),
                new Day25Solver.Point(0, 0, 2, -2),
                new Day25Solver.Point(0, 0, 0, -2),
                new Day25Solver.Point(-1, 2, 0, 0),
                new Day25Solver.Point(-2, -2, -2, 2),
                new Day25Solver.Point(3, 0, 2, -1),
                new Day25Solver.Point(-1, 3, 2, 2),
                new Day25Solver.Point(-1, 0, -1, 0),
                new Day25Solver.Point(0, 2, 1, -2),
                new Day25Solver.Point(3, 0, 0, 0)
            };

            var dataProvider = new Day25PointFileReaderDataProvider(filename);
            var points = dataProvider.GetData();

            Assert.Equal(expectedPoints, points, new PointComparer());
        }

        private class PointComparer : IEqualityComparer<Day25Solver.Point>
        {
            public bool Equals(Day25Solver.Point x, Day25Solver.Point y)
            {
                return x.X == y.X && x.Y == y.Y && x.Z == y.Z && x.W == y.W;
            }

            public int GetHashCode(Day25Solver.Point obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}