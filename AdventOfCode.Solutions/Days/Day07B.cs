
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day7B : Day7A
    {
        public override string Solve()
        {
            var instructions = GetInstructions();
            var numWorkers = 4;
            var second = 0;

            var stepsToWorkOn = new List<Instruction>();
            while (instructions.Count > 0)
            {
                var steps = instructions.Where(i => !i.HasPrerequisites()).OrderBy(s => s.Name).ToList();
                stepsToWorkOn.AddRange(steps
                        .Where(s => !stepsToWorkOn.Contains(s))
                        .Take(Math.Min(steps.Count, Math.Max(0,numWorkers - stepsToWorkOn.Count))));

                var completed = false;
                while (!completed)
                {
                    foreach (var step in stepsToWorkOn)
                    {
                        step.Time--;
                        if (step.Time == 0)
                        {
                            instructions.Remove(step);
                            foreach (var instruction in instructions)
                                instruction.RemovePrerequisite(step);
                            completed = true;
                        }
                    }

                    for (var i = stepsToWorkOn.Count - 1; i >= 0; i--)
                        if(stepsToWorkOn[i].Time == 0)
                            stepsToWorkOn.RemoveAt(i);
                    second++;
                }
            }

            return second.ToString();
        }
    }
}
