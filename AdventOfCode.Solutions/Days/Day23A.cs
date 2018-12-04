using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day23A : IProblem
    {
        protected readonly ParserType Parser;

        public Day23A(ParserType parser) { Parser = parser; }

        public Day23A() : this(new ParserType("day23.in")) { }

        public virtual string Solve()
        {
            return "Unsolved";
        }
    }
}
