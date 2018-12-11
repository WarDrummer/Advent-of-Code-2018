using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day1B : IProblem
    {
        private readonly ParserType _parser;

        public Day1B() : this(new ParserType("day01.in")) { }

        private Day1B(ParserType parser) { _parser = parser; }

        public string Solve()
        {
            var sum = 0;
            var input = _parser.GetData().ToArray();
            var seen = new HashSet<int>();

            while (true)
            {
                foreach (var val in input)
                {
                    sum += int.Parse(val);
                    if (!seen.Contains(sum)) seen.Add(sum);
                    else return sum.ToString();
                }
            }
        }
    }
}
