using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day5A : IProblem
    {
        private readonly ParserType _parser;

        public Day5A(ParserType parser) { _parser = parser; }

        public Day5A() : this(new ParserType("day05.in")) { }

        public virtual string Solve()
        {
            return "Unsolved";
        }
    }
}
