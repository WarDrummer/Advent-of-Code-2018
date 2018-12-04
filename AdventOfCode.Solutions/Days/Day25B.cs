using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day25B : IProblem
    {
        private readonly ParserType _parser;

        public Day25B(ParserType parser)
        {
            _parser = parser;
        }

        public Day25B() : this(new ParserType("day25.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
