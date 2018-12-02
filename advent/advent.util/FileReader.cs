using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent.util
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> ReadStrings(string filename)
        {
            return File.ReadAllLines(filename);
        }
        
        public IEnumerable<int> ReadInts(string filename)
        {
            return File.ReadAllLines(filename).Select(int.Parse);
        }
    }
}