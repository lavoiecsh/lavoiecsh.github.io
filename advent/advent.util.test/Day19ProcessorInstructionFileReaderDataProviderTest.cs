using System.Collections.Generic;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day19ProcessorInstructionFileReaderDataProviderTest
    {
        [Fact]
        public void ReturnsInstructionsInFile()
        {
            const string filename = "data\\day19_instruction_list.txt";
            var dataProvider = new Day19ProcessorInstructionFileReaderDataProvider(filename);
            var program = dataProvider.GetData();
            var expectedInstructions = new List<Day19Solver.ProcessorInstruction>
            {
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.seti, 5, 0, 1),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.seti, 6, 0, 2),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.addi, 0, 1, 0),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.addr, 1, 2, 3),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.setr, 1, 0, 0),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.seti, 8, 0, 4),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.seti, 9, 0, 5)
            };
            Assert.Equal(0, program.InstructionPointer);
            Assert.Equal(expectedInstructions, program.Instructions, new ProcessorInstructionComparer());
        }

        private class ProcessorInstructionComparer : IEqualityComparer<Day19Solver.ProcessorInstruction>
        {
            public bool Equals(Day19Solver.ProcessorInstruction x, Day19Solver.ProcessorInstruction y)
            {
                return x.Operation == y.Operation && x.A == y.A && x.B == y.B && x.C == y.C;
            }

            public int GetHashCode(Day19Solver.ProcessorInstruction obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}