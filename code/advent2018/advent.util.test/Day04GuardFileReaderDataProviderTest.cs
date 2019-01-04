using System.Collections.Generic;
using System.Linq;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day04GuardFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsGuardsFromFile()
        {
            const string filename = "data\\day04_guard_list.txt";
            var guard10 = new Day04Solver.Guard(10);
            guard10.SleepIntervals.Add(new Day04Solver.Guard.SleepInterval(05, 25));
            guard10.SleepIntervals.Add(new Day04Solver.Guard.SleepInterval(30, 55));
            guard10.SleepIntervals.Add(new Day04Solver.Guard.SleepInterval(24, 29));
            var guard99 = new Day04Solver.Guard(99);
            guard99.SleepIntervals.Add(new Day04Solver.Guard.SleepInterval(40, 50));
            guard99.SleepIntervals.Add(new Day04Solver.Guard.SleepInterval(36, 46));
            guard99.SleepIntervals.Add(new Day04Solver.Guard.SleepInterval(45, 55));
            var expected = new[] {guard10, guard99};
            var guards = new Day04GuardFileReaderDataProvider(filename).GetData();
            Assert.Equal(expected, guards, new GuardComparer());
        }
    }

    public class GuardComparer : IEqualityComparer<Day04Solver.Guard>
    {
        public bool Equals(Day04Solver.Guard x, Day04Solver.Guard y)
        {
            return x.Id == y.Id &&
                   x.SleepIntervals.SequenceEqual(y.SleepIntervals, new SleepIntervalComparer());
        }

        public int GetHashCode(Day04Solver.Guard obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class SleepIntervalComparer : IEqualityComparer<Day04Solver.Guard.SleepInterval>
    {
        public bool Equals(Day04Solver.Guard.SleepInterval x, Day04Solver.Guard.SleepInterval y)
        {
            return x.StartMinute == y.StartMinute &&
                   x.EndMinute == y.EndMinute;
        }

        public int GetHashCode(Day04Solver.Guard.SleepInterval obj)
        {
            throw new System.NotImplementedException();
        }
    }
}