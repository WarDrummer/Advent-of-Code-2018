using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day5B : IProblem
    {
        private readonly ParserType _parser;

        public Day5B(ParserType parser) { _parser = parser; }

        public Day5B() : this(new ParserType("day05.in")) { }

        public virtual string Solve()
        {
            return "Unsolved";
        }
    }
}
