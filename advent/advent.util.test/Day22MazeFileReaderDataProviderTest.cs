using Xunit;

namespace advent.util.test
{
    public class Day22MazeFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsMazeInFile()
        {
            const string filename = "data\\day22_maze.txt";
            var maze = new Day22MazeFileReaderDataProvider(filename).GetData();
            Assert.Equal(510, maze.Depth);
            Assert.Equal(10, maze.Target.X);
            Assert.Equal(10, maze.Target.Y);
        }
    }
}