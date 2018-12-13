using Xunit;

namespace advent.util.test
{
    public class Day12PlantCavernFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsPlantCavernInFile()
        {
            const string filename = "data\\day12_plant_cavern.txt";
            var cavern = new Day12PlantCavernFileReaderDataProvider(filename).GetData();
            Assert.Equal("#  # #  ##      ###   ###", cavern.State);
            var expectedNotes = new[]
            {
                "   ##",
                "  #  ",
                " #   ",
                " # # ",
                " # ##",
                " ##  ",
                " ####",
                "# # #",
                "# ###",
                "## # ",
                "## ##",
                "###  ",
                "### #",
                "#### "
            };
            Assert.Equal(expectedNotes, cavern.Notes);
        }
    }
}