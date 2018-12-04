using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day16B : IProblem
    {
        protected readonly ParserType Parser;

        public Day16B(ParserType parser) { Parser = parser; }

        public Day16B() : this(new ParserType("day16.in")) { }
    
        public virtual string Solve()
        {
            return "Unsolved";
        }
    }
}
