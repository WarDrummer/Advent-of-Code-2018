using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day1A : IProblem
    {
        private readonly ParserType _parser;

        public Day1A() : this(new ParserType("day01.in")) { }

        private Day1A(ParserType parser) { _parser = parser; }

        public string Solve()
        {
            var sum = 0;
            var input = _parser.GetData();

            foreach (var val in input)
            {
                sum += int.Parse(val);
            }

            return sum.ToString();
        }
    }
}
