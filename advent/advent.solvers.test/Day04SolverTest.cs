using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day04SolverTest
    {
        private readonly Solver solver;

        public Day04SolverTest()
        {
            var guard10 = new Guard(10);
            guard10.SleepIntervals.Add(new Guard.SleepInterval(05, 25));
            guard10.SleepIntervals.Add(new Guard.SleepInterval(30, 55));
            guard10.SleepIntervals.Add(new Guard.SleepInterval(24, 29));
            var guard99 = new Guard(99);
            guard99.SleepIntervals.Add(new Guard.SleepInterval(40, 50));
            guard99.SleepIntervals.Add(new Guard.SleepInterval(36, 46));
            guard99.SleepIntervals.Add(new Guard.SleepInterval(45, 55));
            var dataProvider = new Mock<DataProvider<Guard>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(new[] {guard10, guard99});

            solver = new Day04Solver(dataProvider.Object);
        }

        [Fact]
        public void ReturnsGuardMostAsleepMultipliedByMinuteMostAsleep()
        {
            Assert.Equal("240", solver.SolveFirstPart());
        }

        [Fact]
        public void ReturnsGuardMostAsleepAtSpecificMinuteMultipliedBySpecificMinuteMostAsleep()
        {
            Assert.Equal("4455", solver.SolveSecondPart());
        }
    }
}