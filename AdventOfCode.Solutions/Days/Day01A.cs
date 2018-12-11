using System.Linq;
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
            var input = _parser.GetData();
            return input.Sum(int.Parse).ToString();
        }
    }
}
