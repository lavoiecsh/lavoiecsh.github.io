using System;
using System.Collections.Generic;

namespace advent.solvers
{
    public class Day19Solver : Solver
    {
        public string ProblemName => "Go With The Flow";

        private readonly DataProvider<Program> dataProvider;

        public Day19Solver(DataProvider<Program> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var program = dataProvider.GetData();
            var processor = new Processor(program.InstructionPointer);
            processor.Execute(program.Instructions);
            return processor.Registers[0].ToString();
        }

        public string SolveSecondPart()
        {
            long r0 = 0;
            const int r1 = 10551383;
            long r2 = 1;

            do
            {
                var r4 = (decimal) r1 / r2;
                if (r4 == Math.Floor(r4)) r0 += r2;
                r2++;
            }
            while (r2 <= r1);

            return r0.ToString();
        }

        public class Processor
        {
            public readonly int[] Registers;
            private readonly int instructionPointer;

            public Processor(int instructionPointer)
            {
                Registers = new [] {0, 0, 0, 0, 0, 0};
                this.instructionPointer = instructionPointer;
            }

            public void Execute(IList<ProcessorInstruction> instructions)
            {
                while (Registers[instructionPointer] < instructions.Count)
                {
                    var instruction = instructions[Registers[instructionPointer]];
                    Execute(instruction.Operation, instruction.A, instruction.B, instruction.C);
                    Registers[instructionPointer]++;
                }
            }

            private void Execute(Operation operation, int a, int b, int c)
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
        }

        public class Program
        {
            public readonly int InstructionPointer;
            public readonly IList<ProcessorInstruction> Instructions;

            public Program(int instructionPointer, IList<ProcessorInstruction> instructions)
            {
                InstructionPointer = instructionPointer;
                Instructions = instructions;
            }
        }

        public class ProcessorInstruction
        {
            public Operation Operation { get; }
            public int A { get; }
            public int B { get; }
            public int C { get; }

            public ProcessorInstruction(Operation operation, int a, int b, int c)
            {
                Operation = operation;
                A = a;
                B = b;
                C = c;
            }
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
}