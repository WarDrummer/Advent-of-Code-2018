using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day21A : IProblem
    {
        private readonly ParserType _parser;

        public Day21A(ParserType parser) { _parser = parser; }

        public Day21A() : this(new ParserType("day21.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
