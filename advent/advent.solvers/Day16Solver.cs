using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day16Solver : Solver
    {
        public string ProblemName => "Chronal Classification";

        private readonly DataProvider<IEnumerable<ProcessorTest>> testDataProvider;
        private readonly DataProvider<IEnumerable<ProcessorOperation>> programDataProvider;

        public Day16Solver(DataProvider<IEnumerable<ProcessorTest>> testDataProvider,
            DataProvider<IEnumerable<ProcessorOperation>> programDataProvider)
        {
            this.testDataProvider = testDataProvider;
            this.programDataProvider = programDataProvider;
        }

        public string SolveFirstPart()
        {
            var tests = testDataProvider.GetData();
            return tests.Count(test => test.GetPossibleOperations().Count() >= 3).ToString();
        }

        public string SolveSecondPart()
        {
            var tests = testDataProvider.GetData().ToList();
            var mapping = new Dictionary<int, Processor.Operation>();
            while (mapping.Count != 16)
            {
                for (var i = tests.Count - 1; i >= 0; --i)
                {
                    var possibleOperations =
                        tests[i].GetPossibleOperations().Where(o => !mapping.ContainsValue(o)).ToList();
                    if (possibleOperations.Count != 1)
                        continue;
                    mapping[tests[i].Operation.Opcode] = possibleOperations.Single();
                    tests.RemoveAt(i);
                }
            }

            var program = programDataProvider.GetData();
            var processor = new Processor();
            foreach (var operation in program)
            {
                processor.Execute(mapping[operation.Opcode], operation.A, operation.B, operation.C);
            }

            return processor.Registers[0].ToString();
        }

        public class Processor
        {
            internal int[] Registers;

            public Processor()
            {
                Registers = new[] {0, 0, 0, 0};
            }

            internal void Execute(Operation operation, int a, int b, int c)
            {
                switch (operation)
                {
                    case Operation.addr:
                        Registers[c] = Registers[a] + Registers[b];
                        break;
                    case Operation.addi:
                        Registers[c] = Registers[a] + b;
                        break;
                    case Operation.mulr:
                        Registers[c] = Registers[a] * Registers[b];
                        break;
                    case Operation.muli:
                        Registers[c] = Registers[a] * b;
                        break;
                    case Operation.banr:
                        Registers[c] = Registers[a] & Registers[b];
                        break;
                    case Operation.bani:
                        Registers[c] = Registers[a] & b;
                        break;
                    case Operation.borr:
                        Registers[c] = Registers[a] | Registers[b];
                        break;
                    case Operation.bori:
                        Registers[c] = Registers[a] | b;
                        break;
                    case Operation.setr:
                        Registers[c] = Registers[a];
                        break;
                    case Operation.seti:
                        Registers[c] = a;
                        break;
                    case Operation.gtir:
                        Registers[c] = a > Registers[b] ? 1 : 0;
                        break;
                    case Operation.gtri:
                        Registers[c] = Registers[a] > b ? 1 : 0;
                        break;
                    case Operation.gtrr:
                        Registers[c] = Registers[a] > Registers[b] ? 1 : 0;
                        break;
                    case Operation.eqir:
                        Registers[c] = a == Registers[b] ? 1 : 0;
                        break;
                    case Operation.eqri:
                        Registers[c] = Registers[a] == b ? 1 : 0;
                        break;
                    case Operation.eqrr:
                        Registers[c] = Registers[a] == Registers[b] ? 1 : 0;
                        break;
                }
            }

            public IEnumerable<Operation> GetPossibleOperations(int a, int b, int c, int[] registers)
            {
                var currentRegisters = new int[4];
                Registers.CopyTo(currentRegisters, 0);
                var possibleOperations = new List<Operation>();
                foreach (var operation in Enum.GetValues(typeof(Operation)).Cast<Operation>())
                {
                    Execute(operation, a, b, c);
                    if (Registers[0] == registers[0] &&
                        Registers[1] == registers[1] &&
                        Registers[2] == registers[2] &&
                        Registers[3] == registers[3])
                        possibleOperations.Add(operation);
                    currentRegisters.CopyTo(Registers, 0);
                }

                return possibleOperations;
            }

            // ReSharper disable InconsistentNaming
            public enum Operation
            {
                addr,
                addi,
                mulr,
                muli,
                banr,
                bani,
                borr,
                bori,
                setr,
                seti,
                gtir,
                gtri,
                gtrr,
                eqir,
                eqri,
                eqrr
            }
        }

        public class ProcessorOperation
        {
            public readonly int Opcode;
            public readonly int A;
            public readonly int B;
            public readonly int C;

            public ProcessorOperation(int opcode, int a, int b, int c)
            {
                Opcode = opcode;
                A = a;
                B = b;
                C = c;
            }
        }

        public class ProcessorTest
        {
            public readonly int[] RegistersBefore;
            public readonly ProcessorOperation Operation;
            public readonly int[] RegistersAfter;

            public Processor.Operation? Code { get; private set; }

            public ProcessorTest(int[] before, ProcessorOperation operation, int[] after)
            {
                RegistersBefore = before;
                Operation = operation;
                RegistersAfter = after;
                Code = null;
            }

            public bool IsCodeSet() => Code != null;

            public void SetCode(Processor.Operation code)
            {
                Code = code;
            }

            public IEnumerable<Processor.Operation> GetPossibleOperations()
            {
                var processor = new Processor {Registers = RegistersBefore};
                return processor.GetPossibleOperations(Operation.A, Operation.B, Operation.C, RegistersAfter);
            }
        }
    }
}