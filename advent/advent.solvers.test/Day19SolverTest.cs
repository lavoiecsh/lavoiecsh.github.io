using System.Collections.Generic;
using Moq;
using Xunit;

namespace advent.solvers.test
{
    public class Day19SolverTest
    {
        private readonly IList<Day19Solver.ProcessorInstruction> instructions;
        private readonly Day19Solver.Processor processor;

        private readonly Solver solver;

        public Day19SolverTest()
        {
            instructions = new List<Day19Solver.ProcessorInstruction>
            {
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.seti, 5, 0, 1),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.seti, 6, 0, 2),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.addi, 0, 1, 0),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.addr, 1, 2, 3),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.setr, 1, 0, 0),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.seti, 8, 0, 4),
                new Day19Solver.ProcessorInstruction(Day19Solver.Operation.seti, 9, 0, 5)
            };
            processor = new Day19Solver.Processor(0);

            var dataProvider = new Mock<DataProvider<Day19Solver.Program>>();
            dataProvider.Setup(dp => dp.GetData()).Returns(new Day19Solver.Program(0, instructions));
            
            solver = new Day19Solver(dataProvider.Object);
        }

        [Fact]
        public void ExecutesOperationsAccordingToInstructionPointer()
        {
            processor.Execute(instructions);
            Assert.Equal(new []{7, 5, 6, 0, 0, 9}, processor.Registers);
        }

        [Fact]
        public void ReturnsValueInRegister0()
        {
            Assert.Equal("7", solver.SolveFirstPart());
        }
    }
}