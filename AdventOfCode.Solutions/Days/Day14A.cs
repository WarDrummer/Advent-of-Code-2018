using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day14A : IProblem
    {
        protected readonly ParserType Parser;

        public Day14A(ParserType parser) { Parser = parser; }

        public Day14A() : this(new ParserType("day14.in")){ }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
