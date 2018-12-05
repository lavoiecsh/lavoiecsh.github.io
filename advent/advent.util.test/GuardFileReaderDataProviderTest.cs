using System.Collections.Generic;
using System.Linq;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class GuardFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsGuardsFromFile()
        {
            const string filename = "guard_list.txt";
            var guard10 = new Guard(10);
            guard10.SleepIntervals.Add(new Guard.SleepInterval(05, 25));
            guard10.SleepIntervals.Add(new Guard.SleepInterval(30, 55));
            guard10.SleepIntervals.Add(new Guard.SleepInterval(24, 29));
            var guard99 = new Guard(99);
            guard99.SleepIntervals.Add(new Guard.SleepInterval(40, 50));
            guard99.SleepIntervals.Add(new Guard.SleepInterval(36, 46));
            guard99.SleepIntervals.Add(new Guard.SleepInterval(45, 55));
            var expected = new[] {guard10, guard99};
            var guards = new GuardFileReaderDataProvider(filename).GetData();
            Assert.Equal(expected, guards, new GuardComparer());
        }
    }

    public class GuardComparer : IEqualityComparer<Guard>
    {
        public bool Equals(Guard x, Guard y)
        {
            return x.Id == y.Id &&
                   x.SleepIntervals.SequenceEqual(y.SleepIntervals, new SleepIntervalComparer());
        }

        public int GetHashCode(Guard obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class SleepIntervalComparer : IEqualityComparer<Guard.SleepInterval>
    {
        public bool Equals(Guard.SleepInterval x, Guard.SleepInterval y)
        {
            return x.StartMinute == y.StartMinute &&
                   x.EndMinute == y.EndMinute;
        }

        public int GetHashCode(Guard.SleepInterval obj)
        {
            throw new System.NotImplementedException();
        }
    }
}