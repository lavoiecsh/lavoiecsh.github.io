using System.Collections.Generic;
using System.Linq;
using advent.solvers;
using Xunit;

namespace advent.util.test
{
    public class Day16ProcessorFileReaderDataProviderTest
    {
        private readonly Day16ProcessorFileReaderDataProvider dataProvider;

        public Day16ProcessorFileReaderDataProviderTest()
        {
            dataProvider = new Day16ProcessorFileReaderDataProvider("data\\day16_processor.txt");
        }

        [Fact]
        public void ReturnsTestOperationsInFile()
        {
            var tests = (dataProvider as DataProvider<IEnumerable<Day16Solver.ProcessorTest>>).GetData();
            var expectedTests = new List<Day16Solver.ProcessorTest>
            {
                new Day16Solver.ProcessorTest(new[] {3, 0, 1, 3},
                    new Day16Solver.ProcessorOperation(15, 2, 1, 3),
                    new[] {3, 0, 1, 1}),
                new Day16Solver.ProcessorTest(new[] {1, 3, 2, 0},
                    new Day16Solver.ProcessorOperation(11, 2, 2, 0),
                    new[] {4, 3, 2, 0}),
                new Day16Solver.ProcessorTest(new[] {0, 3, 3, 1},
                    new Day16Solver.ProcessorOperation(14, 3, 2, 0),
                    new[] {3, 3, 3, 1})
            };
            Assert.Equal(expectedTests, tests, new ProcessorTestComparer());
        }

        [Fact]
        public void ReturnsProgramInFile()
        {
            var program = (dataProvider as DataProvider<IEnumerable<Day16Solver.ProcessorOperation>>).GetData();
            var expectedProgram = new List<Day16Solver.ProcessorOperation>
            {
                new Day16Solver.ProcessorOperation(1, 0, 0, 1),
                new Day16Solver.ProcessorOperation(4, 1, 1, 1),
                new Day16Solver.ProcessorOperation(14, 0, 0, 3)
            };
            Assert.Equal(expectedProgram, program, new ProcessorOperationComparer());
        }
        
        private class ProcessorTestComparer : IEqualityComparer<Day16Solver.ProcessorTest>
        {
            public bool Equals(Day16Solver.ProcessorTest x, Day16Solver.ProcessorTest y)
            {
                return x.RegistersBefore.SequenceEqual(y.RegistersBefore) &&
                       x.RegistersAfter.SequenceEqual(y.RegistersAfter) &&
                       new ProcessorOperationComparer().Equals(x.Operation, y.Operation);
            }

            public int GetHashCode(Day16Solver.ProcessorTest obj)
            {
                throw new System.NotImplementedException();
            }
        }
        
        private class ProcessorOperationComparer : IEqualityComparer<Day16Solver.ProcessorOperation>
        {
            public bool Equals(Day16Solver.ProcessorOperation x, Day16Solver.ProcessorOperation y)
            {
                return x.Opcode == y.Opcode && x.A == y.A && x.B == y.B && x.C == y.C;
            }

            public int GetHashCode(Day16Solver.ProcessorOperation obj)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}