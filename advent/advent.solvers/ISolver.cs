using System.Collections.Generic;

namespace advent.solvers
{
    public interface ISolver
    {
        int Solve1(IEnumerable<string> args);
        int Solve2(IEnumerable<string> args);
    }
}