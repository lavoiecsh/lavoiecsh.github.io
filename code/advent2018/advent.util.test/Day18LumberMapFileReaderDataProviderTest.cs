using System.Text;
using Xunit;

namespace advent.util.test
{
    public class Day18LumberMapFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsLumberMapInFile()
        {
            const string filename = "data\\day18_lumber_map.txt";
            var expectedMap = new StringBuilder()
                .AppendLine(".#.#...|#.")
                .AppendLine(".....#|##|")
                .AppendLine(".|..|...#.")
                .AppendLine("..|#.....#")
                .AppendLine("#.#|||#|#|")
                .AppendLine("...#.||...")
                .AppendLine(".|....|...")
                .AppendLine("||...#|.#|")
                .AppendLine("|.||||..|.")
                .AppendLine("...#.|..|.")
                .ToString();
            var dataProvider = new Day18LumberMapFileReaderDataProvider(filename);
            var map = dataProvider.GetData();
            Assert.Equal(expectedMap, map.ToString());
        }
    }
}