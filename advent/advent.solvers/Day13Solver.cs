using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day13Solver : Solver
    {
        public string ProblemName => "Mine Cart Madness";

        private readonly DataProvider<CartMap> dataProvider;

        public Day13Solver(DataProvider<CartMap> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var firstCrash = FindFirstCrash();
            return $"{firstCrash.X},{firstCrash.Y}";
        }

        public string SolveSecondPart()
        {
            var cartMap = dataProvider.GetData();
            while (cartMap.Carts.Count > 1)
                cartMap.MoveCarts();
            var lastCart = cartMap.Carts.Single();
            return $"{lastCart.X},{lastCart.Y}";
        }

        public (int X, int Y) FindFirstCrash()
        {
            var cartMap = dataProvider.GetData();
            while (true)
            {
                var crashes = cartMap.MoveCarts();
                if (crashes.Count > 0)
                    return crashes.First();
            }
        }

        public class CartMap
        {
            public readonly IDictionary<(int X, int Y), MapTile> Map;
            public readonly IList<Cart> Carts;

            public CartMap(IDictionary<(int X, int Y), MapTile> map, IList<Cart> carts)
            {
                Map = map;
                Carts = carts;
            }

            public IList<(int X, int Y)> MoveCarts()
            {
                var orderedCarts = Carts.OrderBy(cart => cart.X).ThenBy(cart => cart.Y);
                var collisions = new List<(int X, int Y)>();
                foreach (var cart in orderedCarts)
                {
                    cart.Move(this);
                    var collidedCarts = Carts.Where(c => c.X == cart.X && c.Y == cart.Y).ToList();
                    if (collidedCarts.Count == 1) continue;
                    
                    collisions.Add((cart.X, cart.Y));
                    foreach (var collidedCart in collidedCarts)
                        Carts.Remove(collidedCart);
                }
                return collisions;
            }

            public enum MapTile
            {
                Horizontal,
                Vertical,
                CornerSlash,
                CornerBackslash,
                Intersection
            }
        }

        public class Cart
        {
            public CartDirection Direction { get; private set; }
            public int X { get; private set; }
            public int Y { get; private set; }
            private IntersectionTurn nextIntersectionTurn;

            public Cart(int x, int y, CartDirection direction)
            {
                X = x;
                Y = y;
                Direction = direction;
                nextIntersectionTurn = IntersectionTurn.Left;
            }

            public void Move(CartMap cartMap)
            {
                CartMap.MapTile tile;
                switch (Direction)
                {
                    case CartDirection.Left:
                        X--;
                        tile = cartMap.Map[(X, Y)];
                        if (tile == CartMap.MapTile.CornerSlash ||
                            (tile == CartMap.MapTile.Intersection && nextIntersectionTurn == IntersectionTurn.Left))
                            Direction = CartDirection.Down;
                        if (tile == CartMap.MapTile.CornerBackslash ||
                            (tile == CartMap.MapTile.Intersection && nextIntersectionTurn == IntersectionTurn.Right))
                            Direction = CartDirection.Up;
                        if (tile == CartMap.MapTile.Intersection)
                            nextIntersectionTurn = (IntersectionTurn) (((int) nextIntersectionTurn + 1) % 3);
                        break;
                    case CartDirection.Right:
                        X++;
                        tile = cartMap.Map[(X, Y)];
                        if (tile == CartMap.MapTile.CornerSlash ||
                            (tile == CartMap.MapTile.Intersection && nextIntersectionTurn == IntersectionTurn.Left))
                            Direction = CartDirection.Up;
                        if (tile == CartMap.MapTile.CornerBackslash ||
                            (tile == CartMap.MapTile.Intersection && nextIntersectionTurn == IntersectionTurn.Right))
                            Direction = CartDirection.Down;
                        if (tile == CartMap.MapTile.Intersection)
                            nextIntersectionTurn = (IntersectionTurn) (((int) nextIntersectionTurn + 1) % 3);
                        break;
                    case CartDirection.Up:
                        Y--;
                        tile = cartMap.Map[(X, Y)];
                        if (tile == CartMap.MapTile.CornerSlash ||
                            (tile == CartMap.MapTile.Intersection && nextIntersectionTurn == IntersectionTurn.Right))
                            Direction = CartDirection.Right;
                        if (tile == CartMap.MapTile.CornerBackslash ||
                                 (tile == CartMap.MapTile.Intersection && nextIntersectionTurn == IntersectionTurn.Left))
                            Direction = CartDirection.Left;
                        if (tile == CartMap.MapTile.Intersection)
                            nextIntersectionTurn = (IntersectionTurn) (((int) nextIntersectionTurn + 1) % 3);
                        break;
                    case CartDirection.Down:
                        Y++;
                        tile = cartMap.Map[(X, Y)];
                        if (tile == CartMap.MapTile.CornerSlash ||
                            (tile == CartMap.MapTile.Intersection && nextIntersectionTurn == IntersectionTurn.Right))
                            Direction = CartDirection.Left;
                        if (tile == CartMap.MapTile.CornerBackslash ||
                            (tile == CartMap.MapTile.Intersection && nextIntersectionTurn == IntersectionTurn.Left))
                            Direction = CartDirection.Right;
                        if (tile == CartMap.MapTile.Intersection)
                            nextIntersectionTurn = (IntersectionTurn) (((int) nextIntersectionTurn + 1) % 3);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            public enum CartDirection
            {
                Left,
                Right,
                Up,
                Down
            }

            private enum IntersectionTurn
            {
                Left,
                Straight,
                Right
            }
        }
    }
}