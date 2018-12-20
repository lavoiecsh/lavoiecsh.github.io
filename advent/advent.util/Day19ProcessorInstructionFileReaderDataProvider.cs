using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using advent.solvers;

namespace advent.util
{
    public class Day19ProcessorInstructionFileReaderDataProvider : DataProvider<Day19Solver.Program>
    {
        private readonly string filename;

        private readonly Regex ipParsingRegex;
        private readonly Regex programParsingRegex;

        public Day19ProcessorInstructionFileReaderDataProvider(string filename)
        {
            this.filename = filename;
            ipParsingRegex = new Regex("^#ip (\\d)$");
            programParsingRegex = new Regex("^(\\w{4}) (\\d+) (\\d+) (\\d+)");
        }

        public Day19Solver.Program GetData()
        {
            var lines = File.ReadAllLines(filename);
            var ipMatch = ipParsingRegex.Match(lines[0]);
            var instructionPointer = int.Parse(ipMatch.Groups[1].Value);
            var instructions = lines.Skip(1).Select(MakeProcessorInstruction).ToList();
            return new Day19Solver.Program(instructionPointer, instructions);
        }

        private Day19Solver.ProcessorInstruction MakeProcessorInstruction(string instruction)
        {
            var programMatch = programParsingRegex.Match(instruction);
            Enum.TryParse<Day19Solver.Operation>(programMatch.Groups[1].Value, out var operation);
            var a = int.Parse(programMatch.Groups[2].Value);
            var b = int.Parse(programMatch.Groups[3].Value);
            var c = int.Parse(programMatch.Groups[4].Value);
            return new Day19Solver.ProcessorInstruction(operation, a, b, c);
        }
    }
}