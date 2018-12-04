using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day13A : IProblem
    {
        protected readonly ParserType Parser;

        public Day13A(ParserType parser) { Parser = parser; }

        public Day13A() : this(new ParserType("day13.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
