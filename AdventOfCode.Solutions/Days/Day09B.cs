using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day9B : IProblem
    {
        private readonly ParserType _parser;

        public Day9B(ParserType parser) { _parser = parser; }

        public Day9B() : this(new ParserType("day09.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
