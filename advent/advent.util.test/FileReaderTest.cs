using System.Collections.Generic;
using Xunit;

namespace advent.util.test
{
    public class FileReaderTest
    {
        private readonly FileReader fileReader;

        public FileReaderTest()
        {
            fileReader = new FileReader();
        }

        [Fact]
        public void ReturnsListOfStringsFromFile()
        {
            Assert.Equal(new List<string> {"ab", "cde", "fghi"}, fileReader.ReadStrings("string_list.txt"));
        }

        [Fact]
        public void ReturnsListOfIntsFromFile()
        {
            Assert.Equal(new List<int> {1, 2, 3, 4, 5}, fileReader.ReadInts("int_list.txt"));
        }
    }
}