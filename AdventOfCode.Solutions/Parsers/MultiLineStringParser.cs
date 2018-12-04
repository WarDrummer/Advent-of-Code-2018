using System.Collections.Generic;

namespace AdventOfCode.Solutions.Parsers
{
    public class MultiLineStringParser : InputParser<IEnumerable<string>>
    {
        public MultiLineStringParser(string inputFile) : base(inputFile) { }

        public override IEnumerable<string> GetData()
        {
            return GetInput();
        }
    }
}