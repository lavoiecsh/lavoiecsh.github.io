using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day13CartMapFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsMapAndCarts()
        {
            const string filename = "data\\day13_cart_map.txt";

            var dataProvider = new Day13CartMapFileReaderDataProvider(filename);
            var map = dataProvider.GetData();
            var expectedMap = new Dictionary<(int X, int Y), Day13Solver.CartMap.MapTile>
            {
                {(0, 0), Day13Solver.CartMap.MapTile.CornerSlash},
                {(1, 0), Day13Solver.CartMap.MapTile.Horizontal},
                {(2, 0), Day13Solver.CartMap.MapTile.Horizontal},
                {(3, 0), Day13Solver.CartMap.MapTile.Horizontal},
                {(4, 0), Day13Solver.CartMap.MapTile.CornerBackslash},
                {(0, 1), Day13Solver.CartMap.MapTile.Vertical},
                {(4, 1), Day13Solver.CartMap.MapTile.Vertical},
                {(7, 1), Day13Solver.CartMap.MapTile.CornerSlash},
                {(8, 1), Day13Solver.CartMap.MapTile.Horizontal},
                {(9, 1), Day13Solver.CartMap.MapTile.Horizontal},
                {(10, 1), Day13Solver.CartMap.MapTile.Horizontal},
                {(11, 1), Day13Solver.CartMap.MapTile.Horizontal},
                {(12, 1), Day13Solver.CartMap.MapTile.CornerBackslash},
                {(0, 2), Day13Solver.CartMap.MapTile.Vertical},
                {(2, 2), Day13Solver.CartMap.MapTile.CornerSlash},
                {(3, 2), Day13Solver.CartMap.MapTile.Horizontal},
                {(4, 2), Day13Solver.CartMap.MapTile.Intersection},
                {(5, 2), Day13Solver.CartMap.MapTile.Horizontal},
                {(6, 2), Day13Solver.CartMap.MapTile.Horizontal},
                {(7, 2), Day13Solver.CartMap.MapTile.Intersection},
                {(8, 2), Day13Solver.CartMap.MapTile.Horizontal},
                {(9, 2), Day13Solver.CartMap.MapTile.CornerBackslash},
                {(12, 2), Day13Solver.CartMap.MapTile.Vertical},
                {(0, 3), Day13Solver.CartMap.MapTile.Vertical},
                {(2, 3), Day13Solver.CartMap.MapTile.Vertical},
                {(4, 3), Day13Solver.CartMap.MapTile.Vertical},
                {(7, 3), Day13Solver.CartMap.MapTile.Vertical},
                {(9, 3), Day13Solver.CartMap.MapTile.Vertical},
                {(12, 3), Day13Solver.CartMap.MapTile.Vertical},
                {(0, 4), Day13Solver.CartMap.MapTile.CornerBackslash},
                {(1, 4), Day13Solver.CartMap.MapTile.Horizontal},
                {(2, 4), Day13Solver.CartMap.MapTile.Intersection},
                {(3, 4), Day13Solver.CartMap.MapTile.Horizontal},
                {(4, 4), Day13Solver.CartMap.MapTile.CornerSlash},
                {(7, 4), Day13Solver.CartMap.MapTile.CornerBackslash},
                {(8, 4), Day13Solver.CartMap.MapTile.Horizontal},
                {(9, 4), Day13Solver.CartMap.MapTile.Intersection},
                {(10, 4), Day13Solver.CartMap.MapTile.Horizontal},
                {(11, 4), Day13Solver.CartMap.MapTile.Horizontal},
                {(12, 4), Day13Solver.CartMap.MapTile.CornerSlash},
                {(2, 5), Day13Solver.CartMap.MapTile.CornerBackslash},
                {(3, 5), Day13Solver.CartMap.MapTile.Horizontal},
                {(4, 5), Day13Solver.CartMap.MapTile.Horizontal},
                {(5, 5), Day13Solver.CartMap.MapTile.Horizontal},
                {(6, 5), Day13Solver.CartMap.MapTile.Horizontal},
                {(7, 5), Day13Solver.CartMap.MapTile.Horizontal},
                {(8, 5), Day13Solver.CartMap.MapTile.Horizontal},
                {(9, 5), Day13Solver.CartMap.MapTile.CornerSlash}
            };
            Assert.Equal(expectedMap, map.Map);

            var expectedCarts = new[]
            {
                new Day13Solver.Cart(2, 0, Day13Solver.Cart.CartDirection.Right),
                new Day13Solver.Cart(9, 3, Day13Solver.Cart.CartDirection.Down)
            };
            Assert.Equal(expectedCarts, map.Carts, new CartsComparer());
        }

        private class CartsComparer : IEqualityComparer<Day13Solver.Cart>
        {
            public bool Equals(Day13Solver.Cart x, Day13Solver.Cart y)
            {
                return x.X == y.X && x.Y == y.Y && x.Direction == y.Direction;
            }

            public int GetHashCode(Day13Solver.Cart obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}