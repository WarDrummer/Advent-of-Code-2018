using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day9A : IProblem
    {
        private readonly ParserType _parser;

        public Day9A(ParserType parser) { _parser = parser; }

        public Day9A() : this(new ParserType("day09.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
