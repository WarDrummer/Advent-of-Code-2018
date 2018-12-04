using System.Linq;
using System.Text;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day2B : IProblem
    {
        private readonly ParserType _parser;

        public Day2B(ParserType parser) { _parser = parser; }

        public Day2B() : this(new ParserType("day02.in")) { }

        public virtual string Solve()
        {
            var list = _parser.GetData().ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var s1 = list[i];
                for (var j = i + 1; j < list.Count; j++)
                {
                    var diffCount = 0;
                    var s2 = list[j];
                    for (var k = 0; k < s1.Length; k++)
                    {
                        if (s1[k] != s2[k])
                            diffCount++;
                        if (diffCount > 1)
                            break;
                    }

                    if (diffCount == 1)
                    {
                        var sb = new StringBuilder();
                        for (var k = 0; k < s1.Length; k++)
                        {
                            if (s1[k] == s2[k])
                                sb.Append(s1[k]);
                        }
                        return sb.ToString();
                    }
                }
            }

            return "Not Found";
        }
    }
}
