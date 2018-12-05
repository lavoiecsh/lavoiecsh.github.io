using System.Linq;

namespace advent.solvers
{
    public class Day02Solver : Solver
    {
        public string ProblemName => "Inventory Management System";
        
        private readonly DataProvider<string> dataProvider;

        public Day02Solver(DataProvider<string> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var ids = dataProvider.GetData();
            var counts = ids.Select(s => s.GroupBy(c => c).Select(g => g.Count()))
                .Select(cs =>
                {
                    var csl = cs.ToList();
                    return new[] {csl.Contains(2), csl.Contains(3)};
                })
                .ToList();
            return (counts.Count(c => c[0]) * counts.Count(c => c[1])).ToString();
        }

        public string SolveSecondPart()
        {
            var ids = dataProvider.GetData().ToList();
            foreach (var id1 in ids)
            {
                foreach (var id2 in ids)
                {
                    if (id1 == id2)
                        continue;
                    var commonLetters = string.Concat(id1.Zip(id2, (c1, c2) => c1 == c2 ? c1 : ' ').Where(c => c != ' '));
                    if (commonLetters.Length == id1.Length - 1)
                        return commonLetters;
                }
            }

            return null;
        }
    }
}