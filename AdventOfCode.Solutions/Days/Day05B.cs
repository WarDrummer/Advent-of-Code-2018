using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.Parsers;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day5B : Day5A
    {
        private readonly ParserType _parser;

        public Day5B(ParserType parser) { _parser = parser; }

        public Day5B() : this(new ParserType("day05.in")) { }

        public override string Solve()
        {
            var input = _parser.GetData();
            var reversePolarity = CreatePolarityLookup();
            input = ReactPolymerFast(input, reversePolarity);
            var min = int.MaxValue;
   
            for (char lower = 'a', upper = 'A'; lower <= 'z'; lower++, upper++)
            {
                var currentInput = input.RemoveChars(lower, upper);
                var size = ReactPolymerFast(currentInput, reversePolarity).Length;
                if (size < min)
                    min = size;
            }

            return min.ToString();
        }
    }
}
