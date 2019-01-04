using System.Collections.Generic;
using System.IO;
using advent.solvers;

namespace advent.util
{
    public class Day13CartMapFileReaderDataProvider : DataProvider<Day13Solver.CartMap>
    {
        private readonly string filename;

        public Day13CartMapFileReaderDataProvider(string filename)
        {
            this.filename = filename;
        }

        public Day13Solver.CartMap GetData()
        {
            var lines = File.ReadAllLines(filename);
            var map = new Dictionary<(int X, int Y), Day13Solver.CartMap.MapTile>();
            var carts = new List<Day13Solver.Cart>();
            for (var y = 0; y < lines.Length; ++y)
            {
                for (var x = 0; x < lines[y].Length; ++x)
                {
                    switch (lines[y][x])
                    {
                        case '/':
                            map[(x, y)] = Day13Solver.CartMap.MapTile.CornerSlash;
                            break;
                        case '\\':
                            map[(x, y)] = Day13Solver.CartMap.MapTile.CornerBackslash;
                            break;
                        case '>':
                            carts.Add(new Day13Solver.Cart(x, y, Day13Solver.Cart.CartDirection.Right));
                            goto case '-';
                        case '<':
                            carts.Add(new Day13Solver.Cart(x, y, Day13Solver.Cart.CartDirection.Left));
                            goto case '-';
                        case '-':
                            map[(x, y)] = Day13Solver.CartMap.MapTile.Horizontal;
                            break;
                        case '^':
                            carts.Add(new Day13Solver.Cart(x, y, Day13Solver.Cart.CartDirection.Up));
                            goto case '|';
                        case 'v':
                            carts.Add(new Day13Solver.Cart(x, y, Day13Solver.Cart.CartDirection.Down));
                            goto case '|';
                        case '|':
                            map[(x, y)] = Day13Solver.CartMap.MapTile.Vertical;
                            break;
                        case '+':
                            map[(x, y)] = Day13Solver.CartMap.MapTile.Intersection;
                            break;
                    }
                }
            }

            return new Day13Solver.CartMap(map, carts);
        }
    }
}