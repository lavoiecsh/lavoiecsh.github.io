using System.Collections.Generic;
using System.Linq;

namespace advent.solvers
{
    public class Day21Solver : Solver
    {
        public string ProblemName => "Chronal Conversion";

        private readonly DataProvider<Day19Solver.Program> dataProvider;

        public Day21Solver(DataProvider<Day19Solver.Program> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public string SolveFirstPart()
        {
            var program = dataProvider.GetData();
            var processor = new Day19Solver.Processor(program.InstructionPointer);
            while (processor.NextInstruction() < program.Instructions.Count)
            {
                var instruction = program.Instructions[processor.NextInstruction()];
                processor.Execute(instruction);
                processor.IncrementInstructionPointer();
                if (processor.NextInstruction() == 28) return processor.Registers[5].ToString();
            }

            return "";
        }

        public string SolveSecondPart()
        {
            var program = dataProvider.GetData();
            var processor = new Day19Solver.Processor(program.InstructionPointer);

            var register0Stops = new List<int>();
            while (processor.NextInstruction() < program.Instructions.Count)
            {
                var instruction = program.Instructions[processor.NextInstruction()];
                processor.Execute(instruction);
                processor.IncrementInstructionPointer();
                if (processor.NextInstruction() != 28) continue;
                
                if (register0Stops.Contains(processor.Registers[5]))
                    break;
                register0Stops.Add(processor.Registers[5]);
            }
            
            return register0Stops.Last().ToString();
        }
    }
}
