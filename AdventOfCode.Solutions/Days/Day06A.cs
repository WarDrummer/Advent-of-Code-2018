using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day6A : IProblem
    {
        protected readonly ParserType FileParser;

        public Day6A(ParserType fileParser) { FileParser = fileParser; }

        public Day6A() : this(new ParserType("day06.in")) { }

        public virtual string Solve()
        {
            return "Unknown";
        }
    }
}
