using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day16ProcessorFileReaderDataProvider
        : DataProvider<IEnumerable<Day16Solver.ProcessorTest>>,
            DataProvider<IEnumerable<Day16Solver.ProcessorOperation>>
    {
        private readonly string filename;
        private readonly Regex beforeParsingRegex;
        private readonly Regex operationParsingRegex;
        private readonly Regex afterParsingRegex;

        public Day16ProcessorFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            beforeParsingRegex = new Regex("^Before: \\[(\\d+), (\\d+), (\\d+), (\\d+)\\]$");
            operationParsingRegex = new Regex("^(\\d+) (\\d+) (\\d+) (\\d+)$");
            afterParsingRegex = new Regex("^After:  \\[(\\d+), (\\d+), (\\d+), (\\d+)\\]$");
        }

        IEnumerable<Day16Solver.ProcessorTest> DataProvider<IEnumerable<Day16Solver.ProcessorTest>>.GetData()
        {
            var lines = File.ReadAllLines(filename);
            var state = ReadState.Before;
            var tests = new List<Day16Solver.ProcessorTest>();
            int[] before = { };
            Day16Solver.ProcessorOperation test = null;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                if (state == ReadState.Before)
                {
                    var match = beforeParsingRegex.Match(line);
                    if (!match.Success)
                        break;
                    before = new[]
                    {
                        int.Parse(match.Groups[1].Value),
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[3].Value),
                        int.Parse(match.Groups[4].Value)
                    };
                    state = ReadState.Test;
                    continue;
                }

                if (state == ReadState.Test)
                {
                    var match = operationParsingRegex.Match(line);
                    test = new Day16Solver.ProcessorOperation(int.Parse(match.Groups[1].Value),
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[3].Value),
                        int.Parse(match.Groups[4].Value));
                    state = ReadState.After;
                    continue;
                }

                if (state == ReadState.After)
                {
                    var match = afterParsingRegex.Match(line);
                    var after = new[]
                    {
                        int.Parse(match.Groups[1].Value),
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[3].Value),
                        int.Parse(match.Groups[4].Value)
                    };
                    tests.Add(new Day16Solver.ProcessorTest(before, test, after));
                    state = ReadState.Before;
                }
            }

            return tests;
        }

        IEnumerable<Day16Solver.ProcessorOperation> DataProvider<IEnumerable<Day16Solver.ProcessorOperation>>.GetData()
        {
            var lines = File.ReadAllLines(filename);
            var state = ReadState.Before;
            var program = new List<Day16Solver.ProcessorOperation>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                switch (state)
                {
                    case ReadState.Before:
                        if (beforeParsingRegex.IsMatch(line))
                            state = ReadState.Test;
                        else
                        {
                            state = ReadState.Program;
                            goto case ReadState.Program;
                        }

                        break;
                    case ReadState.Test:
                        state = ReadState.After;
                        break;
                    case ReadState.After:
                        state = ReadState.Before;
                        break;
                    case ReadState.Program:
                        var match = operationParsingRegex.Match(line);
                        program.Add(new Day16Solver.ProcessorOperation(int.Parse(match.Groups[1].Value),
                            int.Parse(match.Groups[2].Value),
                            int.Parse(match.Groups[3].Value),
                            int.Parse(match.Groups[4].Value)));
                        break;
                }
            }

            return program;
        }

        private enum ReadState
        {
            Before,
            Test,
            After,
            Program
        }
    }
}