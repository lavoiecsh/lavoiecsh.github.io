using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class SimpleFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsSingleStringInFile()
        {
            const string filename = "data\\string.txt";
            const string expected = "abcde";
            var dataProvider = new SimpleFileReaderDataProvider(filename) as DataProvider<string>;
            Assert.Equal(expected, dataProvider.GetData());
        }
        
        [Fact]
        public void ReturnsIntDataInFile()
        {
            const string filename = "data\\int_list.txt";
            var expected = new[] {1, 2, 3, 4, 5};
            var dataProvider = new SimpleFileReaderDataProvider(filename) as DataProvider<IEnumerable<int>>;
            Assert.Equal(expected, dataProvider.GetData());
        }

        [Fact]
        public void ReturnsStringDataInFile()
        {
            const string filename = "data\\string_list.txt";
            var expected = new[] {"ab", "cde", "fghi"};
            var dataProvider = new SimpleFileReaderDataProvider(filename) as DataProvider<IEnumerable<string>>;
            Assert.Equal(expected, dataProvider.GetData());
        }
    }
}