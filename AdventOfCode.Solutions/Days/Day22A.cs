using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day22A : IProblem
    {
        protected readonly ParserType Parser;

        public Day22A(ParserType parser) { Parser = parser; }

        public Day22A() : this(new ParserType("day22.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
