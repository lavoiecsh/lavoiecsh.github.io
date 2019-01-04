using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day04Solver : Solver
    {
        public string ProblemName => "Repose Record";

        private readonly DataProvider<IEnumerable<Guard>> dataProvider;

        public Day04Solver(DataProvider<IEnumerable<Guard>> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var guards = dataProvider.GetData();
            var mostAsleep = guards.OrderByDescending(guard => guard.TotalMinutesAsleep()).First();
            var minuteMostAsleep = mostAsleep.GetMinuteMostAsleepAndTimes().minute;
            return (mostAsleep.Id * minuteMostAsleep).ToString();
        }

        public string SolveSecondPart()
        {
            var guards = dataProvider.GetData();
            var minutesMostAsleepByGuard = guards.Select(guard =>
                new {guard.Id, MinuteMostAsleepAndTimes = guard.GetMinuteMostAsleepAndTimes()});
            var mostAsleepGuard = minutesMostAsleepByGuard.OrderByDescending(gm => gm.MinuteMostAsleepAndTimes.times)
                .First();
            return (mostAsleepGuard.Id * mostAsleepGuard.MinuteMostAsleepAndTimes.minute).ToString();
        }

        public class Guard
        {
            public readonly int Id;
            public readonly IList<SleepInterval> SleepIntervals;

            public Guard(int id)
            {
                Id = id;
                SleepIntervals = new List<SleepInterval>();
            }

            public int TotalMinutesAsleep()
            {
                return SleepIntervals.Sum(si => si.EndMinute - si.StartMinute);
            }

            public (int minute, int times) GetMinuteMostAsleepAndTimes()
            {
                var minutes = new int[60];
                for (var i = 0; i < 60; ++i)
                    minutes[i] = 0;
                foreach (var si in SleepIntervals)
                    for (var i = si.StartMinute; i < si.EndMinute; ++i)
                        minutes[i]++;
                var maxMinute = 0;
                for (var i = 0; i < 60; ++i)
                {
                    if (minutes[i] > minutes[maxMinute])
                        maxMinute = i;
                }

                return (maxMinute, minutes[maxMinute]);
            }

            public struct SleepInterval
            {
                public readonly int StartMinute;
                public readonly int EndMinute;

                public SleepInterval(int startMinute, int endMinute)
                {
                    StartMinute = startMinute;
                    EndMinute = endMinute;
                }
            }
        }
    }
}