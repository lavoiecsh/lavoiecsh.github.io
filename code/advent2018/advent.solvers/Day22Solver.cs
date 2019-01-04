namespace advent.solvers
{
    public class Day22Solver : Solver
    {
        public string ProblemName => "Mode Maze";
        private readonly DataProvider<Maze> dataProvider;

        public Day22Solver(DataProvider<Maze> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var maze = dataProvider.GetData();
            return maze.RiskLevel().ToString();
        }

        public string SolveSecondPart()
        {
            var maze = dataProvider.GetData();
            var mazeRunner = new MazeRunner(maze);
            var shortestTime = mazeRunner.ShortestTimeTo(maze.Target.X, maze.Target.Y);
            return shortestTime.TimeWithTorch.ToString();
        }

        public class ShortestTime
        {
            private readonly Maze.RegionType type;
            public int? TimeWithTorch;
            public int? TimeWithClimbingGear;
            public int? TimeWithNeither;

            public ShortestTime(Maze.RegionType type)
            {
                this.type = type;
                TimeWithTorch = null;
                TimeWithClimbingGear = null;
                TimeWithNeither = null;
            }

            public bool AddTimes(ShortestTime times)
            {
                var previousTimeWithTorch = TimeWithTorch;
                var previousTimeWithClimbingGear = TimeWithClimbingGear;
                var previousTimeWithNeither = TimeWithNeither;
                switch (type)
                {
                    case Maze.RegionType.Rocky:
                        switch (times.type)
                        {
                            case Maze.RegionType.Rocky:
                                TimeWithTorch = Min(TimeWithTorch, times.TimeWithTorch + 1);
                                TimeWithClimbingGear = Min(TimeWithClimbingGear, times.TimeWithClimbingGear + 1);
                                break;
                            case Maze.RegionType.Wet:
                                TimeWithTorch = Min(TimeWithTorch, times.TimeWithClimbingGear + 8);
                                TimeWithClimbingGear = Min(TimeWithClimbingGear, times.TimeWithClimbingGear + 1);
                                break;
                            case Maze.RegionType.Narrow:
                                TimeWithTorch = Min(TimeWithTorch, times.TimeWithTorch + 1);
                                TimeWithClimbingGear = Min(TimeWithClimbingGear, times.TimeWithTorch + 8);
                                break;
                        }

                        break;
                    case Maze.RegionType.Wet:
                        switch (times.type)
                        {
                            case Maze.RegionType.Rocky:
                                TimeWithClimbingGear = Min(TimeWithClimbingGear, times.TimeWithClimbingGear + 1);
                                TimeWithNeither = Min(TimeWithNeither, times.TimeWithClimbingGear + 8);
                                break;
                            case Maze.RegionType.Wet:
                                TimeWithClimbingGear = Min(TimeWithClimbingGear, times.TimeWithClimbingGear + 1);
                                TimeWithNeither = Min(TimeWithNeither, times.TimeWithNeither + 1);
                                break;
                            case Maze.RegionType.Narrow:
                                TimeWithClimbingGear = Min(TimeWithClimbingGear, times.TimeWithNeither + 8);
                                TimeWithNeither = Min(TimeWithNeither, times.TimeWithNeither + 1);
                                break;
                        }

                        break;
                    case Maze.RegionType.Narrow:
                        switch (times.type)
                        {
                            case Maze.RegionType.Rocky:
                                TimeWithTorch = Min(TimeWithTorch, times.TimeWithTorch + 1);
                                TimeWithNeither = Min(TimeWithNeither, times.TimeWithTorch + 8);
                                break;
                            case Maze.RegionType.Wet:
                                TimeWithTorch = Min(TimeWithTorch, times.TimeWithNeither + 8);
                                TimeWithNeither = Min(TimeWithNeither, times.TimeWithNeither + 1);
                                break;
                            case Maze.RegionType.Narrow:
                                TimeWithTorch = Min(TimeWithTorch, times.TimeWithTorch + 1);
                                TimeWithNeither = Min(TimeWithNeither, times.TimeWithNeither + 1);
                                break;
                        }

                        break;
                }

                return previousTimeWithTorch != TimeWithTorch ||
                       previousTimeWithClimbingGear != TimeWithClimbingGear ||
                       previousTimeWithNeither != TimeWithNeither;
            }

            private static int? Min(int? time1, int? time2)
            {
                if (!time1.HasValue) return time2;
                if (!time2.HasValue) return time1;
                return time1.Value < time2.Value ? time1 : time2;
            }
        }

        private class MazeRunner
        {
            private readonly ShortestTime[,] shortestTimes;

            public MazeRunner(Maze maze)
            {
                shortestTimes = new ShortestTime[maze.RegionTypes.GetLength(0), maze.RegionTypes.GetLength(1)];
                for (var x = 0; x < shortestTimes.GetLength(0); ++x)
                for (var y = 0; y < shortestTimes.GetLength(1); ++y)
                    shortestTimes[x, y] = new ShortestTime(maze.RegionTypes[x, y]);
                shortestTimes[0, 0].TimeWithTorch = 0;
                shortestTimes[0, 0].TimeWithClimbingGear = 7;
                var changed = ComputeShortestTimes();
                while (changed) changed = ComputeShortestTimes();
            }

            internal ShortestTime ShortestTimeTo(int x, int y)
            {
                return shortestTimes[x, y];
            }

            private bool ComputeShortestTimes()
            {
                var maxX = shortestTimes.GetLength(0) - 1;
                var maxY = shortestTimes.GetLength(1) - 1;
                var changed = false;
                for (var x = 0; x <= maxX; ++x)
                {
                    for (var y = 0; y <= maxY; ++y)
                    {
                        var times = shortestTimes[x, y];
                        if (x > 0)
                            changed |= times.AddTimes(shortestTimes[x - 1, y]);
                        if (x < maxX)
                            changed |= times.AddTimes(shortestTimes[x + 1, y]);
                        if (y > 0)
                            changed |= times.AddTimes(shortestTimes[x, y - 1]);
                        if (y < maxY)
                            changed |= times.AddTimes(shortestTimes[x, y + 1]);
                    }
                }

                return changed;
            }
        }

        public class Maze
        {
            private const int Multiplier = 3;
            
            public readonly int Depth;
            public readonly (int X, int Y) Target;

            private readonly long[,] erosionLevels;
            public readonly RegionType[,] RegionTypes;

            public Maze(int depth, (int X, int Y) target)
            {
                Depth = depth;
                Target = target;
                erosionLevels = new long[Target.X * Multiplier, Target.Y * Multiplier];
                BuildErosionLevels();
                RegionTypes = new RegionType[Target.X * Multiplier, Target.Y * Multiplier];
                BuildRegionTypes();
            }

            private void BuildErosionLevels()
            {
                for (var y = 0; y < erosionLevels.GetLength(1); ++y)
                for (var x = 0; x < erosionLevels.GetLength(0); ++x)
                    erosionLevels[x, y] = GetErosionLevel(x, y);
            }

            private long GetErosionLevel(int x, int y)
            {
                if (x == 0 &&
                    y == 0) return 0;
                if (x == Target.X &&
                    y == Target.Y) return 0;
                if (y == 0) return (x * 16807 + Depth) % 20183;
                if (x == 0) return (y * 48271 + Depth) % 20183;
                return (erosionLevels[x - 1, y] * erosionLevels[x, y - 1] + Depth) % 20183;
            }

            private void BuildRegionTypes()
            {
                for (var y = 0; y < RegionTypes.GetLength(1); ++y)
                for (var x = 0; x < RegionTypes.GetLength(0); ++x)
                    RegionTypes[x, y] = GetRegionType(x, y);
            }

            internal RegionType GetRegionType(int x, int y)
            {
                return (RegionType) ((erosionLevels[x, y] + Depth) % 3);
            }

            public enum RegionType
            {
                Rocky = 0,
                Wet = 1,
                Narrow = 2
            }

            internal int RiskLevel()
            {
                var riskLevel = 0;
                for (var y = 0; y <= Target.Y; ++y)
                for (var x = 0; x <= Target.X; ++x)
                    riskLevel += (int) RegionTypes[x, y];

                return riskLevel;
            }
        }
    }
}