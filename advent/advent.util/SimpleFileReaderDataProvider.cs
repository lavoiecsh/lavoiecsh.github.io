using System.Collections.Generic;
using System.IO;
using System.Linq;
using advent.solvers;

namespace advent.util
{
    public class SimpleFileReaderDataProvider : DataProvider<string>, DataProvider<int>
    {
        private readonly string filename;

        public SimpleFileReaderDataProvider(string filename)
        {
            this.filename = filename;
        }
        
        IEnumerable<string> DataProvider<string>.GetData()
        {
            return File.ReadAllLines(filename);
        }

        IEnumerable<int> DataProvider<int>.GetData()
        {
            return File.ReadAllLines(filename).Select(int.Parse);
        }
    }
}