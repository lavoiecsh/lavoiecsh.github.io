using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class ClaimFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsClaimsFromFile()
        {
            const string filename = "data\\claim_list.txt";
            var expected = new[]
            {
                new Claim(1, 1, 3, 4, 4),
                new Claim(2, 3, 1, 4, 4),
                new Claim(3, 5, 5, 2, 2)
            };
            var claims = new ClaimFileReaderDataProvider(filename).GetData();
            Assert.Equal(expected, claims, new ClaimComparer());
        }
    }

    public class ClaimComparer : IEqualityComparer<Claim>
    {
        public bool Equals(Claim x, Claim y)
        {
            return x.Id == y.Id &&
                   x.Top == y.Top &&
                   x.Left == y.Left &&
                   x.Bottom == y.Bottom &&
                   x.Right == y.Right;
        }

        public int GetHashCode(Claim obj)
        {
            throw new System.NotImplementedException();
        }
    }
}