using System.Collections.Generic;

namespace advent.util
{
    public interface IFileReader
    {
        IEnumerable<string> ReadStrings(string fileTxt);
        IEnumerable<int> ReadInts(string filename);
    }
}