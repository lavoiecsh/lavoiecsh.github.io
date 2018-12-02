using System.Collections.Generic;

namespace advent.util
{
    public interface IFileReader
    {
        IEnumerable<int> ReadInts(string filename);
    }
}