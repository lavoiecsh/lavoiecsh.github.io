using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class ClaimFileReaderDataProvider : DataProvider<Claim>
    {
        private readonly string filename;
        private readonly Regex claimParsingRegex;

        public ClaimFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            claimParsingRegex = new Regex("^#(\\d+) @ (\\d+),(\\d+): (\\d+)x(\\d+)$");
        }

        public IEnumerable<Claim> GetData()
        {
            return File.ReadAllLines(filename).Select(MakeClaim);
        }

        private Claim MakeClaim(string line)
        {
            var match = claimParsingRegex.Match(line);
            var id = int.Parse(match.Groups[1].Value);
            var left = int.Parse(match.Groups[2].Value);
            var top = int.Parse(match.Groups[3].Value);
            var width = int.Parse(match.Groups[4].Value);
            var height = int.Parse(match.Groups[5].Value);
            return new Claim(id, left, top, width, height);
        }
    }
}