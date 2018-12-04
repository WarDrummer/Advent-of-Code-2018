using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day11A : IProblem
    {
        protected readonly ParserType _parser;

        public Day11A(ParserType parser) { _parser = parser; }

        public Day11A() : this(new ParserType("day11.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
