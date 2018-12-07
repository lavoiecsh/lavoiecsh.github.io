using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent.solvers
{
    public class Day07Solver : Solver
    {
        public string ProblemName => "The Sum of Its Parts";

        private readonly DataProvider<Step> dataProvider;
        private readonly int workerCount;

        public Day07Solver(DataProvider<Step> dataProvider, int workerCount = 5)
        {
            this.dataProvider = dataProvider;
            this.workerCount = workerCount;
        }

        public string SolveFirstPart()
        {
            var steps = dataProvider.GetData().ToList();
            var order = new StringBuilder();
            while (steps.Any(s => !s.IsCompleted))
            {
                var nextStep = steps.First(s => !s.IsCompleted && s.Requirements.All(r => r.IsCompleted));
                nextStep.IsCompleted = true;
                order.Append(nextStep.Id);
            }

            return order.ToString();
        }

        public string SolveSecondPart()
        {
            var steps = dataProvider.GetData().ToList();
            var workers = Enumerable.Range(1, workerCount).Select(_ => new Worker()).ToList();
            var totalTime = 0;
            while (steps.Any(s => !s.IsCompleted))
            {
                AssignSteps(workers, steps);
                foreach (var worker in workers)
                    worker.ReduceTime();
                totalTime++;
            }

            return totalTime.ToString();
        }

        private static void AssignSteps(IEnumerable<Worker> workers, IList<Step> steps)
        {
            var idleWorkers = workers.Where(w => !w.IsWorking);
            foreach (var worker in idleWorkers)
            {
                var assignableSteps = steps
                    .Where(s => !s.IsCompleted && !s.IsStarted && s.Requirements.All(r => r.IsCompleted))
                    .ToList();
                if (!assignableSteps.Any())
                    return;
                worker.StartStep(assignableSteps.First());
            }
        }

        public class Step
        {
            public readonly char Id;
            public readonly IList<Step> Requirements;
            public bool IsCompleted { get; set; }

            public readonly int RequiredTime;
            public int RemainingTime { get; private set; }
            public bool IsStarted { get; private set; }

            public Step(char id, int minimumTime)
            {
                Id = id;
                Requirements = new List<Step>();
                IsCompleted = false;

                RequiredTime = minimumTime + (Id - 64);
                IsStarted = false;
            }

            public void Start()
            {
                RemainingTime = RequiredTime;
                IsStarted = true;
            }

            public void ReduceTime()
            {
                RemainingTime--;
                if (RemainingTime != 0) return;
                IsStarted = false;
                IsCompleted = true;
            }
        }

        private class Worker
        {
            private Step currentStep;

            public Worker()
            {
                currentStep = null;
            }

            public bool IsWorking => currentStep != null;

            public void StartStep(Step step)
            {
                currentStep = step;
                step.Start();
            }

            public void ReduceTime()
            {
                if (currentStep == null) return;
                currentStep.ReduceTime();
                if (currentStep.IsCompleted)
                    currentStep = null;
            }
        }
    }
}