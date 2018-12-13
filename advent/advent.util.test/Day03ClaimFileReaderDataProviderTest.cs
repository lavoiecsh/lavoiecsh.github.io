using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day03ClaimFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsClaimsFromFile()
        {
            const string filename = "data\\day03_claim_list.txt";
            var expected = new[]
            {
                new Day03Solver.Claim(1, 1, 3, 4, 4),
                new Day03Solver.Claim(2, 3, 1, 4, 4),
                new Day03Solver.Claim(3, 5, 5, 2, 2)
            };
            var claims = new Day03ClaimFileReaderDataProvider(filename).GetData();
            Assert.Equal(expected, claims, new ClaimComparer());
        }
    }

    public class ClaimComparer : IEqualityComparer<Day03Solver.Claim>
    {
        public bool Equals(Day03Solver.Claim x, Day03Solver.Claim y)
        {
            return x.Id == y.Id &&
                   x.Top == y.Top &&
                   x.Left == y.Left &&
                   x.Bottom == y.Bottom &&
                   x.Right == y.Right;
        }

        public int GetHashCode(Day03Solver.Claim obj)
        {
            throw new System.NotImplementedException();
        }
    }
}