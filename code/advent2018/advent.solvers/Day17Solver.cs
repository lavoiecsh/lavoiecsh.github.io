using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent.solvers
{
    public class Day17Solver : Solver
    {
        public string ProblemName => "Reservoir Research";

        private readonly DataProvider<WaterMap> dataProvider;

        public Day17Solver(DataProvider<WaterMap> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var waterMap = dataProvider.GetData();
            waterMap.CalculateWaterFlow();
            Console.WriteLine(waterMap.ToString());
            return waterMap.WaterTileCount().ToString();
        }

        public string SolveSecondPart()
        {
            var waterMap = dataProvider.GetData();
            waterMap.CalculateWaterFlow();
            return waterMap.RestingWaterTileCount().ToString();
        }

        public class WaterMap
        {
            private readonly MapTile[,] map;

            private readonly IList<(int X, int Y)> waterFlows;

            public WaterMap(IList<(int X, int Y)> clayPatches)
            {
                var minY = clayPatches.Min(p => p.Y);
                map = new MapTile[clayPatches.Max(p => p.Y) + 1 - minY, clayPatches.Max(p => p.X) + 2];
                for (var i = 0; i < map.GetLength(0); ++i)
                for (var j = 0; j < map.GetLength(1); ++j)
                    map[i, j] = MapTile.Sand;
                foreach (var clayPatch in clayPatches)
                    map[clayPatch.Y - minY, clayPatch.X] = MapTile.Clay;
                map[0, 500] = MapTile.Spring;
                waterFlows = new List<(int X, int Y)> {(500, 0)};
            }

            public int WaterTileCount()
            {
                var count = 0;
                for (var i = 0; i < map.GetLength(0); ++i)
                {
                    for (var j = 0; j < map.GetLength(1); ++j)
                    {
                        if (map[i, j] == MapTile.Flowing ||
                            map[i, j] == MapTile.Resting)
                            count++;
                    }
                }

                return count;
            }

            public int RestingWaterTileCount()
            {
                var count = 0;
                for (var i = 0; i < map.GetLength(0); ++i)
                {
                    for (var j = 0; j < map.GetLength(1); ++j)
                    {
                        if (map[i, j] == MapTile.Resting)
                            count++;
                    }
                }

                return count;
            }

            internal void CalculateWaterFlow()
            {
                while (waterFlows.Any())
                {
                    var waterFlow = waterFlows.First();
                    waterFlows.RemoveAt(0);
                    CalculateWaterFlow(waterFlow.X, waterFlow.Y);
                }
            }

            internal void CalculateWaterFlow(int x, int y)
            {
                if (y == map.GetLength(0) - 1)
                    return;
                if (map[y + 1, x] == MapTile.Sand)
                {
                    map[y + 1, x] = MapTile.Flowing;
                    waterFlows.Add((x, y + 1));
                    return;
                }

                if (map[y + 1, x] != MapTile.Clay &&
                    map[y + 1, x] != MapTile.Resting)
                    return;

                Fill(x, y);
            }

            private void Fill(int x, int y)
            {
                while (true)
                {
                    var leftWall = false;
                    var leftStop = -1;
                    var rightWall = false;
                    var rightStop = -1;
                    for (var i = x; i >= 0; --i)
                    {
                        if (map[y + 1, i] == MapTile.Sand)
                        {
                            leftStop = i;
                            break;
                        }

                        if (map[y, i - 1] == MapTile.Clay)
                        {
                            leftWall = true;
                            leftStop = i;
                            break;
                        }
                    }

                    for (var i = x; i < map.GetLength(1); ++i)
                    {
                        if (map[y + 1, i] == MapTile.Sand)
                        {
                            rightStop = i;
                            break;
                        }

                        if (map[y, i + 1] == MapTile.Clay)
                        {
                            rightWall = true;
                            rightStop = i;
                            break;
                        }
                    }

                    if (leftWall && rightWall)
                    {
                        for (var i = leftStop; i <= rightStop; ++i) map[y, i] = MapTile.Resting;
                        y = y - 1;
                        continue;
                    }

                    for (var i = leftStop; i <= rightStop; ++i) map[y, i] = MapTile.Flowing;
                    if (!leftWall) waterFlows.Add((leftStop, y));
                    if (!rightWall) waterFlows.Add((rightStop, y));
                    break;
                }
            }

            public override string ToString()
            {
                var builder = new StringBuilder();
                var minX = map.GetLength(1);
                for (var i = 0; i < map.GetLength(0); ++i)
                {
                    for (var j = 0; j < map.GetLength(1); ++j)
                    {
                        if (map[i, j] == MapTile.Sand ||
                            j >= minX) continue;
                        minX = j;
                        break;
                    }
                }

                for (var i = 0; i < map.GetLength(0); ++i)
                {
                    for (var j = minX - 1; j < map.GetLength(1); ++j)
                        builder.Append((char) map[i, j]);

                    builder.AppendLine();
                }

                return builder.ToString();
            }
        }

        private enum MapTile
        {
            Sand = '.',
            Clay = '#',
            Resting = '~',
            Flowing = '|',
            Spring = '+'
        }
    }
}