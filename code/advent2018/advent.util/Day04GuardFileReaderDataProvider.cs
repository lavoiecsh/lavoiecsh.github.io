using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day04GuardFileReaderDataProvider : DataProvider<IEnumerable<Day04Solver.Guard>>
    {
        private readonly string filename;

        public Day04GuardFileReaderDataProvider(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<Day04Solver.Guard> GetData()
        {
            var lines = File.ReadAllLines(filename).ToList();
            lines.Sort();
            var reader = new GuardDataReader();
            foreach (var line in lines)
                reader.Parse(line);
            return reader.GetGuards();
        }

        private class GuardDataReader
        {
            private readonly IList<Day04Solver.Guard> guards;
            private Day04Solver.Guard currentGuard;
            private int fallsAsleepMinute;
            private readonly Regex guardBeginsShiftParsingRegex;
            private readonly Regex guardFallsAsleepParsingRegex;
            private readonly Regex guardWakesUpParsingRegex;

            internal GuardDataReader()
            {
                guards = new List<Day04Solver.Guard>();
                currentGuard = null;
                fallsAsleepMinute = 0;
                const string timestampRegexFormat = "\\[(\\d{4})-(\\d{2})-(\\d{2}) (\\d{2}):(\\d{2})\\]";
                guardBeginsShiftParsingRegex = new Regex($"^{timestampRegexFormat} Guard #(\\d+) begins shift$");
                guardFallsAsleepParsingRegex = new Regex($"^{timestampRegexFormat} falls asleep$");
                guardWakesUpParsingRegex = new Regex($"^{timestampRegexFormat} wakes up");
            }

            internal IEnumerable<Day04Solver.Guard> GetGuards()
            {
                return guards;
            }

            internal void Parse(string line)
            {
                var beginsShiftMatch = guardBeginsShiftParsingRegex.Match(line);
                var fallsAsleepMatch = guardFallsAsleepParsingRegex.Match(line);
                var wakesUpMatch = guardWakesUpParsingRegex.Match(line);
                if (beginsShiftMatch.Success) HandleBeginsShift(beginsShiftMatch);
                if (fallsAsleepMatch.Success) HandleFallsAsleep(fallsAsleepMatch);
                if (wakesUpMatch.Success) HandleWakesUp(wakesUpMatch);
            }

            private void HandleBeginsShift(Match match)
            {
                var id = int.Parse(match.Groups[6].Value);
                currentGuard = guards.SingleOrDefault(g => g.Id == id);
                if (currentGuard != null) return;
                currentGuard = new Day04Solver.Guard(id);
                guards.Add(currentGuard);
            }

            private void HandleFallsAsleep(Match match)
            {
                fallsAsleepMinute = match.Groups[5].Value == "00" ? 0 : int.Parse(match.Groups[5].Value);
            }

            private void HandleWakesUp(Match match)
            {
                var wakesUpMinute = match.Groups[5].Value == "00" ? 0 : int.Parse(match.Groups[5].Value);
                currentGuard.SleepIntervals.Add(new Day04Solver.Guard.SleepInterval(fallsAsleepMinute, wakesUpMinute));
            }
        }
    }
}