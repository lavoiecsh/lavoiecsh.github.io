using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day13SolverTest
    {
        private readonly Mock<DataProvider<Day13Solver.CartMap>> dataProvider;
        private readonly Day13Solver solver;
        private readonly Day13Solver.CartMap firstSample;
        private readonly Day13Solver.CartMap secondSample;

        private readonly CartComparer cartComparer;

        public Day13SolverTest()
        {
            dataProvider = new Mock<DataProvider<Day13Solver.CartMap>>();
            firstSample = new Day13Solver.CartMap(new Dictionary<(int X, int Y), Day13Solver.CartMap.MapTile>
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
                },
                new List<Day13Solver.Cart>
                {
                    new Day13Solver.Cart(2, 0, Day13Solver.Cart.CartDirection.Right),
                    new Day13Solver.Cart(9, 3, Day13Solver.Cart.CartDirection.Down)
                });
            secondSample = new Day13Solver.CartMap(new Dictionary<(int X, int Y), Day13Solver.CartMap.MapTile>
                {
                    {(0, 0), Day13Solver.CartMap.MapTile.CornerBackslash},
                    {(1,0), Day13Solver.CartMap.MapTile.Horizontal},
                    {(2,0), Day13Solver.CartMap.MapTile.Horizontal},
                    {(3,0), Day13Solver.CartMap.MapTile.Horizontal},
                    {(4,0), Day13Solver.CartMap.MapTile.CornerBackslash},
                    {(0,1), Day13Solver.CartMap.MapTile.Vertical},
                    {(4,1), Day13Solver.CartMap.MapTile.Vertical},
                    {(0,2), Day13Solver.CartMap.MapTile.Vertical},
                    {(2,2), Day13Solver.CartMap.MapTile.CornerSlash},
                    {(3,2), Day13Solver.CartMap.MapTile.Horizontal},
                    {(4,2), Day13Solver.CartMap.MapTile.Intersection},
                    {(5,2), Day13Solver.CartMap.MapTile.Horizontal},
                    {(6,2), Day13Solver.CartMap.MapTile.CornerBackslash},
                    {(0,3), Day13Solver.CartMap.MapTile.Vertical},
                    {(2,3), Day13Solver.CartMap.MapTile.Vertical},
                    {(4,3), Day13Solver.CartMap.MapTile.Vertical},
                    {(6,3), Day13Solver.CartMap.MapTile.Vertical},
                    {(0,4), Day13Solver.CartMap.MapTile.CornerBackslash},
                    {(1,4), Day13Solver.CartMap.MapTile.Horizontal},
                    {(2,4), Day13Solver.CartMap.MapTile.Intersection},
                    {(3,4),Day13Solver.CartMap.MapTile.Horizontal},
                    {(4,4), Day13Solver.CartMap.MapTile.CornerSlash},
                    {(6,4), Day13Solver.CartMap.MapTile.Vertical},
                    {(2,5), Day13Solver.CartMap.MapTile.Vertical},
                    {(6,5), Day13Solver.CartMap.MapTile.Vertical},
                    {(2,6), Day13Solver.CartMap.MapTile.CornerBackslash},
                    {(3,6), Day13Solver.CartMap.MapTile.Horizontal},
                    {(4,6), Day13Solver.CartMap.MapTile.Horizontal},
                    {(5,6), Day13Solver.CartMap.MapTile.Horizontal},
                    {(6,6), Day13Solver.CartMap.MapTile.CornerSlash}
                },
                new List<Day13Solver.Cart>
                {
                    new Day13Solver.Cart(1, 0, Day13Solver.Cart.CartDirection.Right),
                    new Day13Solver.Cart(3, 0, Day13Solver.Cart.CartDirection.Left),
                    new Day13Solver.Cart(3, 2, Day13Solver.Cart.CartDirection.Left),
                    new Day13Solver.Cart(1, 4, Day13Solver.Cart.CartDirection.Right),
                    new Day13Solver.Cart(3, 4, Day13Solver.Cart.CartDirection.Left),
                    new Day13Solver.Cart(6, 3, Day13Solver.Cart.CartDirection.Down),
                    new Day13Solver.Cart(6, 5, Day13Solver.Cart.CartDirection.Up),
                    new Day13Solver.Cart(3, 6, Day13Solver.Cart.CartDirection.Left),
                    new Day13Solver.Cart(5, 6, Day13Solver.Cart.CartDirection.Right)
                });

            solver = new Day13Solver(dataProvider.Object);

            cartComparer = new CartComparer();
        }

        [Fact]
        public void ReturnsLocationOfFirstCrash()
        {
            var firstCrash = solver.FindFirstCrash();
            Assert.Equal(7, firstCrash.X);
            Assert.Equal(3, firstCrash.Y);
        }

        [Fact]
        public void MovesCart()
        {
            var cart = firstSample.Carts.First();
            Assert.Equal(new Day13Solver.Cart(2, 0, Day13Solver.Cart.CartDirection.Right), cart, cartComparer);

            cart.Move(firstSample);
            Assert.Equal(new Day13Solver.Cart(3, 0, Day13Solver.Cart.CartDirection.Right), cart, cartComparer);

            cart.Move(firstSample);
            Assert.Equal(new Day13Solver.Cart(4, 0, Day13Solver.Cart.CartDirection.Down), cart, cartComparer);

            cart.Move(firstSample);
            Assert.Equal(new Day13Solver.Cart(4, 1, Day13Solver.Cart.CartDirection.Down), cart, cartComparer);
        }

        [Fact]
        public void MovesCartInIntersection()
        {
            var cart = firstSample.Carts.First();
            for (var i = 0; i < 4; ++i)
                cart.Move(firstSample);
            Assert.Equal(Day13Solver.Cart.CartDirection.Right, cart.Direction);

            for (var i = 0; i < 3; ++i)
                cart.Move(firstSample);
            Assert.Equal(Day13Solver.Cart.CartDirection.Right, cart.Direction);

            for (var i = 0; i < 4; ++i)
                cart.Move(firstSample);
            Assert.Equal(Day13Solver.Cart.CartDirection.Left, cart.Direction);
        }

        [Fact]
        public void MovesCarts()
        {
            firstSample.MoveCarts();
            Assert.Equal(new Day13Solver.Cart(3, 0, Day13Solver.Cart.CartDirection.Right),
                firstSample.Carts[0],
                cartComparer);
            Assert.Equal(new Day13Solver.Cart(9, 4, Day13Solver.Cart.CartDirection.Right),
                firstSample.Carts[1],
                cartComparer);
        }

        [Fact]
        public void ReturnsCollisions()
        {
            for (var i = 0; i < 13; ++i)
                firstSample.MoveCarts();
            var collisions = firstSample.MoveCarts();
            var expectedCollisions = new[] {(7, 3)};
            Assert.Equal(expectedCollisions, collisions);
        }

        [Fact]
        public void ReturnsLocationOfCrash()
        {
            dataProvider.Setup(dp => dp.GetData()).Returns(firstSample);
            Assert.Equal("7,3", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsLocationOfLastRemainingCart()
        {
            dataProvider.Setup(dp => dp.GetData()).Returns(secondSample);
            Assert.Equal("6,4", solver.SolveSecondPart());
        }

        [Fact]
        public void RemovesCollidedCarts()
        {
            for (var i = 0; i < 14; ++i)
                firstSample.MoveCarts();
            Assert.Equal(0, firstSample.Carts.Count);
            
            Assert.Equal(9, secondSample.Carts.Count);
            secondSample.MoveCarts();
            Assert.Equal(3, secondSample.Carts.Count);
            secondSample.MoveCarts();
            secondSample.MoveCarts();
            Assert.Equal(1, secondSample.Carts.Count);
        }

        private class CartComparer : IEqualityComparer<Day13Solver.Cart>
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