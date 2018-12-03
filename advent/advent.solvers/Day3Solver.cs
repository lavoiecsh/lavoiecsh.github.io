using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using advent.util;

namespace advent.solvers
{
    public class Day3Solver : ISolver
    {
        public string ProblemName => "No Matter How You Slice It";

        private readonly IFileReader fileReader;

        public Day3Solver(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public string Solve1(IEnumerable<string> args)
        {
            var claims = fileReader.ReadStrings(args.First()).Select(s => new Claim(s)).ToList();
            var canvas = new Canvas(claims.Max(c => c.Right), claims.Max(c => c.Bottom));
            foreach (var claim in claims)
                canvas.AddClaim(claim);
            return canvas.NumberOfSquaresWithOverlap().ToString();
        }

        public string Solve2(IEnumerable<string> args)
        {
            var claims = fileReader.ReadStrings(args.First()).Select(s => new Claim(s)).ToList();
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
                    foreach (var conflictingClaim in canvas[i,j])
                        claim.ConflictsWith(conflictingClaim);
                    canvas[i, j].Add(claim);
                }
            }

            public int NumberOfSquaresWithOverlap()
            {
                return canvas.Cast<List<Claim>>().Count(claims => claims.Count > 1);
            }
        }

        private class Claim
        {
            public readonly int Id;
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
            public readonly int Width;
            public readonly int Height;

            private int conflicts;

            public Claim(string definition)
            {
                var regex = new Regex("^#(\\d+) @ (\\d+),(\\d+): (\\d+)x(\\d+)$");
                var match = regex.Match(definition);
                Id = int.Parse(match.Groups[1].Value);
                Left = int.Parse(match.Groups[2].Value);
                Top = int.Parse(match.Groups[3].Value);
                Width = int.Parse(match.Groups[4].Value);
                Height = int.Parse(match.Groups[5].Value);
                Right = Left + Width;
                Bottom = Top + Height;
                conflicts = 0;
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