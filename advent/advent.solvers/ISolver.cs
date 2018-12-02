using System.Collections.Generic;

namespace advent.solvers
{
    public interface ISolver
    {
        string Solve1(IEnumerable<string> args);
        string Solve2(IEnumerable<string> args);
    }
}