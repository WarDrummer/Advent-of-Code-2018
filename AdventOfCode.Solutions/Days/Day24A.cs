using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day24A : IProblem
    {
        protected readonly ParserType Parser;

        public Day24A(ParserType parser) { Parser = parser; }

        public Day24A() : this(new ParserType("day24.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
