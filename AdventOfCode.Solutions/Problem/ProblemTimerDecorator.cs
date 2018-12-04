using System;
using System.Diagnostics;

namespace AdventOfCode.Solutions.Problem
{
    internal class ProblemTimerDecorator : IProblem
    {
        private readonly IProblem _problem;

        public ProblemTimerDecorator(IProblem problem)
        {
            _problem = problem;
        }

        public string Solve()
        {
            var sw = new Stopwatch();
            string result;
            try
            {
                sw.Start();
                result = _problem.Solve();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            finally
            {
                sw.Stop();
            }

            return $"{result}\n\r{sw.Elapsed} ({sw.ElapsedMilliseconds}ms)";
        }
    }
}
