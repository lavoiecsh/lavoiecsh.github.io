using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class LightFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsLightsFromFile()
        {
            const string filename = "data\\light_list.txt";
            var expected = new List<Day10Solver.Light>
            {
                new Day10Solver.Light(9, 1, 0, 2),
                new Day10Solver.Light(7, 0, -1, 0),
                new Day10Solver.Light(3, -2, -1, 1),
                new Day10Solver.Light(6, 10, -2, -1),
                new Day10Solver.Light(2, -4, 2, 2),
                new Day10Solver.Light(-6, 10, 2, -2),
                new Day10Solver.Light(1, 8, 1, -1),
                new Day10Solver.Light(1, 7, 1, 0),
                new Day10Solver.Light(-3, 11, 1, -2),
                new Day10Solver.Light(7, 6, -1, -1),
                new Day10Solver.Light(-2, 3, 1, 0),
                new Day10Solver.Light(-4, 3, 2, 0),
                new Day10Solver.Light(10, -3, -1, 1),
                new Day10Solver.Light(5, 11, 1, -2),
                new Day10Solver.Light(4, 7, 0, -1),
                new Day10Solver.Light(8, -2, 0, 1),
                new Day10Solver.Light(15, 0, -2, 0),
                new Day10Solver.Light(1, 6, 1, 0),
                new Day10Solver.Light(8, 9, 0, -1),
                new Day10Solver.Light(3, 3, -1, 1),
                new Day10Solver.Light(0, 5, 0, -1),
                new Day10Solver.Light(-2, 2, 2, 0),
                new Day10Solver.Light(5, -2, 1, 2),
                new Day10Solver.Light(1, 4, 2, 1),
                new Day10Solver.Light(-2, 7, 2, -2),
                new Day10Solver.Light(3, 6, -1, -1),
                new Day10Solver.Light(5, 0, 1, 0),
                new Day10Solver.Light(-6, 0, 2, 0),
                new Day10Solver.Light(5, 9, 1, -2),
                new Day10Solver.Light(14, 7, -2, 0),
                new Day10Solver.Light(-3, 6, 2, -1)
            };
            Assert.Equal(expected, new LightFileReaderDataProvider(filename).GetData(), new LightComparer());
        }

        private class LightComparer : IEqualityComparer<Day10Solver.Light>
        {
            public bool Equals(Day10Solver.Light x, Day10Solver.Light y)
            {
                return x.Position.X == y.Position.X &&
                       x.Position.Y == y.Position.Y &&
                       x.Velocity.X == y.Velocity.X &&
                       x.Velocity.Y == y.Velocity.Y;
            }

            public int GetHashCode(Day10Solver.Light obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}