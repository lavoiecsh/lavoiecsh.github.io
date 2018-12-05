namespace advent.solvers
{
    public interface Solver
    {
        string ProblemName { get; }
        string SolveFirstPart();
        string SolveSecondPart();
    }
}