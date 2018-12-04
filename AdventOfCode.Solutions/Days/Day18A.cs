using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day18A : IProblem
    {
        protected readonly ParserType Parser;

        public Day18A(ParserType parser) { Parser = parser; }

        public Day18A() : this(new ParserType("day18.in")) { }

        public virtual string Solve()
        {
            return "Unsolved";
        }
    }
}
