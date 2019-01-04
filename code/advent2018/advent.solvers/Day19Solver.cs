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
                while (NextInstruction() < instructions.Count)
                {
                    Execute(instructions[NextInstruction()]);
                    IncrementInstructionPointer();
                }
            }

            internal int NextInstruction()
            {
                return Registers[instructionPointer];
            }

            internal void IncrementInstructionPointer()
            {
                Registers[instructionPointer]++;
            }

            internal void Execute(ProcessorInstruction instruction)
            {
                switch (instruction.Operation)
                {
                    case Operation.addr:
                        Registers[instruction.C] = Registers[instruction.A] + Registers[instruction.B];
                        break;
                    case Operation.addi:
                        Registers[instruction.C] = Registers[instruction.A] + instruction.B;
                        break;
                    case Operation.mulr:
                        Registers[instruction.C] = Registers[instruction.A] * Registers[instruction.B];
                        break;
                    case Operation.muli:
                        Registers[instruction.C] = Registers[instruction.A] * instruction.B;
                        break;
                    case Operation.banr:
                        Registers[instruction.C] = Registers[instruction.A] & Registers[instruction.B];
                        break;
                    case Operation.bani:
                        Registers[instruction.C] = Registers[instruction.A] & instruction.B;
                        break;
                    case Operation.borr:
                        Registers[instruction.C] = Registers[instruction.A] | Registers[instruction.B];
                        break;
                    case Operation.bori:
                        Registers[instruction.C] = Registers[instruction.A] | instruction.B;
                        break;
                    case Operation.setr:
                        Registers[instruction.C] = Registers[instruction.A];
                        break;
                    case Operation.seti:
                        Registers[instruction.C] = instruction.A;
                        break;
                    case Operation.gtir:
                        Registers[instruction.C] = instruction.A > Registers[instruction.B] ? 1 : 0;
                        break;
                    case Operation.gtri:
                        Registers[instruction.C] = Registers[instruction.A] > instruction.B ? 1 : 0;
                        break;
                    case Operation.gtrr:
                        Registers[instruction.C] = Registers[instruction.A] > Registers[instruction.B] ? 1 : 0;
                        break;
                    case Operation.eqir:
                        Registers[instruction.C] = instruction.A == Registers[instruction.B] ? 1 : 0;
                        break;
                    case Operation.eqri:
                        Registers[instruction.C] = Registers[instruction.A] == instruction.B ? 1 : 0;
                        break;
                    case Operation.eqrr:
                        Registers[instruction.C] = Registers[instruction.A] == Registers[instruction.B] ? 1 : 0;
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