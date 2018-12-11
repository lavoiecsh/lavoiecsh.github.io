using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day11Solver : Solver
    {
        public string ProblemName => "Chronal Charge";

        private readonly int serialNumber;

        public Day11Solver(int serialNumber)
        {
            this.serialNumber = serialNumber;
        }

        public string SolveFirstPart()
        {
            var powerGrid = new PowerGrid(serialNumber);
            var cellGrid = powerGrid.FindHighestLevelCellGrid(3);
            return $"{cellGrid.X},{cellGrid.Y}";
        }

        public string SolveSecondPart()
        {
            var powerGrid = new PowerGrid(serialNumber);
            var splitPowerGrid = new SplitPowerGrid(powerGrid);
            var cellGrid = splitPowerGrid.FindHighestLevelCellGrid();
            return $"{cellGrid.X},{cellGrid.Y},{cellGrid.Size}";
        }

        public static class Cell
        {
            public static int ComputePowerLevel(int serialNumber, int x, int y)
            {
                var rackId = x + 10;
                var powerLevel = rackId * y;
                powerLevel += serialNumber;
                powerLevel *= rackId;
                var hundreds = (powerLevel / 100) % 10;
                return hundreds - 5;
            }
        }

        public class CellGrid : IComparable<CellGrid>
        {
            public readonly int X;
            public readonly int Y;
            public int Size { get; private set; }
            public int PowerLevel { get; private set; }

            private readonly PowerGrid powerGrid;

            public CellGrid(PowerGrid powerGrid, int x, int y, int size)
            {
                X = x;
                Y = y;
                Size = size;
                this.powerGrid = powerGrid;
                PowerLevel = SumCellPowerLevels(powerGrid.Cells, x, y, size);
            }

            private CellGrid(CellGrid cellGrid)
            {
                X = cellGrid.X;
                Y = cellGrid.Y;
                Size = cellGrid.Size;
                PowerLevel = cellGrid.PowerLevel;
                powerGrid = cellGrid.powerGrid;
            }

            public CellGrid IncreaseSize()
            {
                var nextSize = new CellGrid(this);
                nextSize.Size++;
                nextSize.PowerLevel +=
                    Enumerable.Range(nextSize.X, nextSize.Size)
                        .Sum(x => nextSize.powerGrid.Cells[x, nextSize.Y + nextSize.Size - 1]) +
                    Enumerable.Range(nextSize.Y, nextSize.Size - 1)
                        .Sum(y => nextSize.powerGrid.Cells[nextSize.X + nextSize.Size - 1, y]);
                return nextSize;
            }

            private static int SumCellPowerLevels(int[,] cells, int x, int y, int size)
            {
                return Enumerable.Range(x, size).Sum(x2 => Enumerable.Range(y, size).Sum(y2 => cells[x2, y2]));
            }

            public int CompareTo(CellGrid other)
            {
                return PowerLevel.CompareTo(other.PowerLevel);
            }

            public static bool operator >(CellGrid lhs, CellGrid rhs)
            {
                return lhs.CompareTo(rhs) > 0;
            }

            public static bool operator <(CellGrid lhs, CellGrid rhs)
            {
                return lhs.CompareTo(rhs) < 0;
            }
        }

        public class PowerGrid
        {
            public readonly int[,] Cells;

            public PowerGrid(int serialNumber)
            {
                Cells = new int[301, 301];
                for (var i = 1; i <= 300; ++i)
                for (var j = 1; j <= 300; ++j)
                    Cells[i, j] = Cell.ComputePowerLevel(serialNumber, i, j);
            }

            public CellGrid GetCellGrid(int x, int y, int size)
            {
                return new CellGrid(this, x, y, size);
            }

            private IEnumerable<CellGrid> CalculatePowerLevels(int size)
            {
                var cellGrids = new List<CellGrid>();
                for (var i = 1; i <= 301 - size; ++i)
                for (var j = 1; j <= 301 - size; ++j)
                    cellGrids.Add(GetCellGrid(i, j, size));
                return cellGrids;
            }

            public CellGrid FindHighestLevelCellGrid(int size)
            {
                return CalculatePowerLevels(size).Max();
            }
        }

        public class SplitPowerGrid
        {
            private readonly CellGrid[,] cellGrids;

            private int size;

            public SplitPowerGrid(PowerGrid powerGrid)
            {
                size = 1;
                cellGrids = new CellGrid[301, 301];
                for (var i = 1; i <= 301 - size; ++i)
                for (var j = 1; j <= 301 - size; ++j)
                    cellGrids[i, j] = powerGrid.GetCellGrid(i, j, size);
            }

            public CellGrid FindHighestLevelCellGrid()
            {
                var best = FindHighestCurrentCellGrid();
                for (var s = 2; s < 300; ++s)
                {
                    CalculateNextSize();
                    var currentBest = FindHighestCurrentCellGrid();
                    if (currentBest > best)
                        best = currentBest;
                }

                return best;
            }

            private CellGrid FindHighestCurrentCellGrid()
            {
                return cellGrids.Cast<CellGrid>().Max();
            }

            private void CalculateNextSize()
            {
                size++;
                for (var i = 1; i <= 301 - size; ++i)
                for (var j = 1; j <= 301 - size; ++j)
                    cellGrids[i, j] = cellGrids[i, j].IncreaseSize();
            }
        }
    }
}