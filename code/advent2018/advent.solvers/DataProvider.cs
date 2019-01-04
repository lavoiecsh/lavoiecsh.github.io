namespace advent.solvers
{
    public interface DataProvider<out T>
    {
        T GetData();
    }
}