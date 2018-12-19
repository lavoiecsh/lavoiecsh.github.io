using System;
using System.Diagnostics;
using System.Text;

namespace advent.solvers
{
    public class Day18Solver : Solver
    {
        public string ProblemName => "Settlers of the North Pole";

        private readonly DataProvider<LumberMap> dataProvider;

        public Day18Solver(DataProvider<LumberMap> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var lumberMap = dataProvider.GetData();
            lumberMap.Iterate(10);
            var trees = lumberMap.Count(AcreType.Trees);
            var lumberyards = lumberMap.Count(AcreType.Lumberyard);
            return (trees * lumberyards).ToString();
        }

        public string SolveSecondPart()
        {
            var lumberMap = dataProvider.GetData();
            lumberMap.Iterate(1_000_000_000);
            var trees = lumberMap.Count(AcreType.Trees);
            var lumberyards = lumberMap.Count(AcreType.Lumberyard);
            return (trees * lumberyards).ToString();
        }

        public class LumberMap
        {
            private readonly AcreType[,] acres;

            public LumberMap(AcreType[,] acres)
            {
                this.acres = acres;
            }

            public override string ToString()
            {
                var builder = new StringBuilder();
                for (var i = 0; i < acres.GetLength(0); ++i)
                {
                    for (var j = 0; j < acres.GetLength(1); ++j)
                        builder.Append((char) acres[i, j]);
                    builder.AppendLine();
                }

                return builder.ToString();
            }

            internal void Iterate()
            {
                var next = new AcreType[acres.GetLength(0), acres.GetLength(1)];
                for (var i = 0; i < acres.GetLength(0); ++i)
                for (var j = 0; j < acres.GetLength(1); ++j)
                    next[i, j] = GetNext(i, j);

                Array.Copy(next, acres, next.Length);
            }

            private AcreType GetNext(int i, int j)
            {
                switch (acres[i, j])
                {
                    case AcreType.OpenGround:
                        return IsSurroundedByThreeTrees(i, j) ? AcreType.Trees : AcreType.OpenGround;
                    case AcreType.Trees:
                        return IsSurroundedByThreeLumberyards(i, j) ? AcreType.Lumberyard : AcreType.Trees;
                    case AcreType.Lumberyard:
                        return IsSurroundedByTreeAndLumberyard(i, j) ? AcreType.Lumberyard : AcreType.OpenGround;
                    default:
                        return AcreType.OpenGround;
                }
            }

            private bool IsSurroundedByThreeTrees(int i, int j)
            {
                return Count(i, j, AcreType.Trees) >= 3;
            }

            private bool IsSurroundedByThreeLumberyards(int i, int j)
            {
                return Count(i, j, AcreType.Lumberyard) >= 3;
            }

            private bool IsSurroundedByTreeAndLumberyard(int i, int j)
            {
                return Count(i, j, AcreType.Trees) >= 1 && Count(i, j, AcreType.Lumberyard) >= 1;
            }

            private int Count(int i, int j, AcreType acreType)
            {
                var count = 0;
                var hasRowAbove = j > 0;
                var hasRowBelow = j < acres.GetLength(1) - 1;
                if (i > 0)
                {
                    if (hasRowAbove && acres[i - 1, j - 1] == acreType) count++;
                    if (acres[i - 1, j] == acreType) count++;
                    if (hasRowBelow && acres[i - 1, j + 1] == acreType) count++;
                }

                if (hasRowAbove && acres[i, j - 1] == acreType) count++;
                if (hasRowBelow && acres[i, j + 1] == acreType) count++;

                if (i < acres.GetLength(0) - 1)
                {
                    if (hasRowAbove && acres[i + 1, j - 1] == acreType) count++;
                    if (acres[i + 1, j] == acreType) count++;
                    if (hasRowBelow && acres[i + 1, j + 1] == acreType) count++;
                }

                return count;
            }

            internal void Iterate(int times)
            {
                var watch = new Stopwatch();
                watch.Start();
                for (var i = 0; i < times; ++i)
                {
                    if (i % 1000 == 0)
                        Console.WriteLine($"{i}: {watch.Elapsed.Seconds}");
                    Iterate();
                }
            }

            internal int Count(AcreType type)
            {
                var count = 0;
                for (var i = 0; i < acres.GetLength(0); ++i)
                    for (var j = 0; j < acres.GetLength(1); ++j)
                        if (acres[i, j] == type)
                            count++;
                return count;
            }
        }

        public enum AcreType
        {
            OpenGround = '.',
            Trees = '|',
            Lumberyard = '#'
        }
    }
}