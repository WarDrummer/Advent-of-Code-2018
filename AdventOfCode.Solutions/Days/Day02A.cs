using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day2A : IProblem
    {
        private readonly ParserType _parser;

        public Day2A(ParserType parser) { _parser = parser; }

        public Day2A() : this(new ParserType("day02.in")) { }

        public virtual string Solve()
        {
            var twoCount = 0;
            var threeCount = 0;
            var letterCount = new Dictionary<char, int>();

            foreach (var s in _parser.GetData())
            {
                letterCount.Clear();
                foreach(var c in s)
                {
                    if (!letterCount.ContainsKey(c))
                        letterCount[c] = 0;
                    letterCount[c]++;
                }

                if (letterCount.Values.Any(cnt => cnt == 2))
                    twoCount++;

                if (letterCount.Values.Any(cnt => cnt == 3))
                    threeCount++;

            }
            return (twoCount * threeCount).ToString();
        }
    }
}
