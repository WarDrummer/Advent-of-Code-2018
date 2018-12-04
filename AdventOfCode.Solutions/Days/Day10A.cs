using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day10A : IProblem
    {
        protected readonly ParserType Parser;

        public Day10A(ParserType parser) { Parser = parser; }

        public Day10A() : this(new ParserType("day10.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }       
    }
}
