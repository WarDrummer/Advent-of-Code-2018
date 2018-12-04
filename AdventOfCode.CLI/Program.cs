using System;
using AdventOfCode.Solutions.Days;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            ProblemFactory.Create<Day1A>()
                .SendToClipboard()
                .AppendTime()
                .Solve()
                .ToConsole("Day 1A");

            ProblemFactory.Create<Day1B>()
                .SendToClipboard()
                .AppendTime()
                .Solve()
                .ToConsole("Day 1B");

            ProblemFactory.Create<Day2A>()
                .SendToClipboard()
                .AppendTime()
                .Solve()
                .ToConsole("Day 2A");

            ProblemFactory.Create<Day2B>()
                .SendToClipboard()
                .AppendTime()
                .Solve()
                .ToConsole("Day 2B");

            ProblemFactory.Create<Day3A>()
                .SendToClipboard()
                .AppendTime()
                .Solve()
                .ToConsole("Day 3A");

            ProblemFactory.Create<Day3B>()
                .SendToClipboard()
                .AppendTime()
                .Solve()
                .ToConsole("Day 3B");

            ProblemFactory.Create<Day4A>()
                .SendToClipboard()
                .AppendTime()
                .Solve()
                .ToConsole("Day 4A");

            ProblemFactory.Create<Day4B>()
                .SendToClipboard()
                .AppendTime()
                .Solve()
                .ToConsole("Day 4B");

            Console.ReadKey();
        }
    }
}
