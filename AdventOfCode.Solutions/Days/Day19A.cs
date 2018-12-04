using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day19A : IProblem
    {
        protected readonly ParserType Parser;

        public Day19A(ParserType parser) { Parser = parser; }

        public Day19A() : this(new ParserType("day19.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
