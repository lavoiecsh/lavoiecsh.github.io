using System.Collections.Generic;

namespace advent.solvers
{
    public interface DataProvider<out T>
    {
        IEnumerable<T> GetData();
    }
}