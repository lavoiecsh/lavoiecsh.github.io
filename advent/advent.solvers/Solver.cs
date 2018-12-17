using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("advent.solvers.test")]
namespace advent.solvers
{
    public interface Solver
    {
        string ProblemName { get; }
        string SolveFirstPart();
        string SolveSecondPart();
    }
}
