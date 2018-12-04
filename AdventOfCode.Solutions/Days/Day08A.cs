using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day8A : IProblem
    {
        protected readonly ParserType _parser;

        public Day8A(ParserType parser) { _parser = parser; }

        public Day8A() : this(new ParserType("day08.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
