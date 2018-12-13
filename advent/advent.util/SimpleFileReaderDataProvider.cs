using System.Collections.Generic;
using System.IO;
using System.Linq;
using advent.solvers;

namespace advent.util
{
    public class SimpleFileReaderDataProvider : DataProvider<string>, DataProvider<IEnumerable<string>>, DataProvider<IEnumerable<int>>
    {
        private readonly string filename;

        public SimpleFileReaderDataProvider(string filename)
        {
            this.filename = filename;
        }

        string DataProvider<string>.GetData()
        {
            return File.ReadAllText(filename);
        }
        
        IEnumerable<string> DataProvider<IEnumerable<string>>.GetData()
        {
            return File.ReadAllLines(filename);
        }

        IEnumerable<int> DataProvider<IEnumerable<int>>.GetData()
        {
            return File.ReadAllLines(filename).Select(int.Parse);
        }
    }
}