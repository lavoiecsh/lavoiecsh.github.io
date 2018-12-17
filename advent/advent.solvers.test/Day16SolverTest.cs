using Xunit;

namespace advent.solvers.test
{
    public class Day16SolverTest
    {
        private readonly Day16Solver.Processor processor;

        public Day16SolverTest()
        {
            processor = new Day16Solver.Processor {Registers = {[0] = 2, [1] = 3, [2] = 1}};
        }

        [Fact]
        public void AddsRegister()
        {
            processor.Execute(Day16Solver.Processor.Operation.addr, 0, 1, 2);
            Assert.Equal(new[] {2, 3, 5, 0}, processor.Registers);
        }

        [Fact]
        public void AddsImmediate()
        {
            processor.Execute(Day16Solver.Processor.Operation.addi, 0, 2, 1);
            Assert.Equal(new[] {2, 4, 1, 0}, processor.Registers);
        }

        [Fact]
        public void MultipliesRegister()
        {
            processor.Execute(Day16Solver.Processor.Operation.mulr, 0, 1, 2);
            Assert.Equal(new[] {2, 3, 6, 0}, processor.Registers);
        }

        [Fact]
        public void MultiplesImmediate()
        {
            processor.Execute(Day16Solver.Processor.Operation.muli, 0, 4, 2);
            Assert.Equal(new[] {2, 3, 8, 0}, processor.Registers);
        }

        [Fact]
        public void AndsRegister()
        {
            processor.Execute(Day16Solver.Processor.Operation.banr, 0, 1, 2);
            Assert.Equal(new[] {2, 3, 2, 0}, processor.Registers);
        }

        [Fact]
        public void AndsImmediate()
        {
            processor.Execute(Day16Solver.Processor.Operation.bani, 0, 6, 3);
            Assert.Equal(new[] {2, 3, 1, 2}, processor.Registers);
        }

        [Fact]
        public void OrsRegister()
        {
            processor.Execute(Day16Solver.Processor.Operation.borr, 0, 1, 2);
            Assert.Equal(new[] {2, 3, 3, 0}, processor.Registers);
        }

        [Fact]
        public void OrsImmediate()
        {
            processor.Execute(Day16Solver.Processor.Operation.bori, 0, 5, 3);
            Assert.Equal(new[] {2, 3, 1, 7}, processor.Registers);
        }

        [Fact]
        public void AssignsRegister()
        {
            processor.Execute(Day16Solver.Processor.Operation.setr, 0, 4, 2);
            Assert.Equal(new[] {2, 3, 2, 0}, processor.Registers);
        }

        [Fact]
        public void AssignsImmediate()
        {
            processor.Execute(Day16Solver.Processor.Operation.seti, 0, 4, 2);
            Assert.Equal(new[] {2, 3, 0, 0}, processor.Registers);
        }

        [Fact]
        public void GreaterThanTestsImmediateRegister()
        {
            processor.Execute(Day16Solver.Processor.Operation.gtir, 1, 0, 3);
            Assert.Equal(new[] {2, 3, 1, 0}, processor.Registers);
            processor.Execute(Day16Solver.Processor.Operation.gtir, 4, 0, 3);
            Assert.Equal(new[] {2, 3, 1, 1}, processor.Registers);
        }

        [Fact]
        public void GreaterThanTestsRegisterImmediate()
        {
            processor.Execute(Day16Solver.Processor.Operation.gtri, 0, 4, 3);
            Assert.Equal(new[] {2, 3, 1, 0}, processor.Registers);
            processor.Execute(Day16Solver.Processor.Operation.gtri, 0, 1, 3);
            Assert.Equal(new[] {2, 3, 1, 1}, processor.Registers);
        }

        [Fact]
        public void GreaterThanTestsRegisterRegister()
        {
            processor.Execute(Day16Solver.Processor.Operation.gtrr, 0, 1, 3);
            Assert.Equal(new[] {2, 3, 1, 0}, processor.Registers);
            processor.Execute(Day16Solver.Processor.Operation.gtrr, 1, 0, 3);
            Assert.Equal(new[] {2, 3, 1, 1}, processor.Registers);
        }

        [Fact]
        public void EqualTestsImmediateRegister()
        {
            processor.Execute(Day16Solver.Processor.Operation.eqir, 4, 0, 3);
            Assert.Equal(new[] {2, 3, 1, 0}, processor.Registers);
            processor.Execute(Day16Solver.Processor.Operation.eqir, 2, 0, 3);
            Assert.Equal(new[] {2, 3, 1, 1}, processor.Registers);
        }

        [Fact]
        public void EqualTestsRegisterImmediate()
        {
            processor.Execute(Day16Solver.Processor.Operation.eqri, 0, 4, 3);
            Assert.Equal(new[] {2, 3, 1, 0}, processor.Registers);
            processor.Execute(Day16Solver.Processor.Operation.eqri, 0, 2, 3);
            Assert.Equal(new[] {2, 3, 1, 1}, processor.Registers);
        }

        [Fact]
        public void EqualTestsRegisterRegister()
        {
            processor.Execute(Day16Solver.Processor.Operation.eqrr, 0, 1, 3);
            Assert.Equal(new[] {2, 3, 1, 0}, processor.Registers);
            processor.Execute(Day16Solver.Processor.Operation.eqrr, 0, 0, 3);
            Assert.Equal(new[] {2, 3, 1, 1}, processor.Registers);
        }

        [Fact]
        public void CountsPossibleOperations()
        {
            processor.Registers = new[] {3, 2, 1, 1};
            var count = processor.GetPossibleOperations(2, 1, 2, new[] {3, 2, 2, 1});
            Assert.Equal(3, count);
        }
    }
}