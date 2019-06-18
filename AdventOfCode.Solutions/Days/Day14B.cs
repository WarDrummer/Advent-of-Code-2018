using System;

namespace AdventOfCode.Solutions.Days
{
    public class Day14B : Day14A
    {
        public override string Solve()
        {
            var puzzleInput = Parser.GetData();

            Initialize();

            for(var i = 0; i < 21000000; i++)
                Iterate();

            var result = Print().IndexOf(puzzleInput, StringComparison.Ordinal);

            return result.ToString();
        }
    }
}
