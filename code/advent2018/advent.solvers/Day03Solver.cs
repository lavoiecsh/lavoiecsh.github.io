using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day03Solver : Solver
    {
        public string ProblemName => "No Matter How You Slice It";

        private readonly DataProvider<IEnumerable<Claim>> dataProvider;

        public Day03Solver(DataProvider<IEnumerable<Claim>> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var claims = dataProvider.GetData().ToList();
            var canvas = new Canvas(claims.Max(c => c.Right), claims.Max(c => c.Bottom));
            foreach (var claim in claims)
                canvas.AddClaim(claim);
            return canvas.NumberOfSquaresWithOverlap().ToString();
        }

        public string SolveSecondPart()
        {
            var claims = dataProvider.GetData().ToList();
            var canvas = new Canvas(claims.Max(c => c.Right), claims.Max(c => c.Bottom));
            foreach (var claim in claims)
                canvas.AddClaim(claim);
            return claims.Single(claim => claim.HasNoConflicts()).Id.ToString();
        }

        private class Canvas
        {
            private readonly IList<Claim>[,] canvas;

            public Canvas(int width, int height)
            {
                canvas = new IList<Claim>[width + 1, height + 1];
                for (var i = 0; i < canvas.GetLength(0); ++i)
                for (var j = 0; j < canvas.GetLength(1); ++j)
                    canvas[i, j] = new List<Claim>();
            }

            public void AddClaim(Claim claim)
            {
                for (var i = claim.Left; i < claim.Right; ++i)
                for (var j = claim.Top; j < claim.Bottom; ++j)
                {
                    foreach (var conflictingClaim in canvas[i, j])
                        claim.ConflictsWith(conflictingClaim);
                    canvas[i, j].Add(claim);
                }
            }

            public int NumberOfSquaresWithOverlap()
            {
                return canvas.Cast<List<Claim>>().Count(claims => claims.Count > 1);
            }
        }

        public class Claim
        {
            public readonly int Id;
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;

            private int conflicts;

            public Claim(int id, int left, int top, int width, int height)
            {
                Id = id;
                Left = left;
                Top = top;
                Right = Left + width;
                Bottom = Top + height;
            }

            public void ConflictsWith(Claim otherClaim)
            {
                conflicts++;
                otherClaim.conflicts++;
            }

            public bool HasNoConflicts()
            {
                return conflicts == 0;
            }
        }
    }
}