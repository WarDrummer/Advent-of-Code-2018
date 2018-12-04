using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day7B : IProblem
    {
        private readonly ParserType _parser;

        public Day7B(ParserType parser) { _parser = parser; }

        public Day7B() : this(new ParserType("day07.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
