using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day12A : IProblem
    {
        protected readonly ParserType Parser;

        public Day12A(ParserType parser) { Parser = parser; }

        public Day12A() : this(new ParserType("day12.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
