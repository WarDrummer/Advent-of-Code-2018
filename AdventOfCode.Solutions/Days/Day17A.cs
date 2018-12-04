using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day17A : IProblem
    {
        protected readonly ParserType Parser;

        public Day17A(ParserType parser) { Parser = parser; }

        public Day17A() : this(new ParserType("day17.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
