using AdventOfCode.Solutions.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day18B : Day18A
    {
        public override string Solve()
        {
            var one = Parser.GetData().Select(s => s.ToArray()).ToArray();
            var two = Parser.GetData().Select(s => s.ToArray()).ToArray();

            char[][][] pages = { one, two };
            var prev = 0;
            var next = 1;
            var lookup = new Dictionary<int, string>();
            var answers = new List<string>();

            var limit = 1000000000;
            var i = 0;
            var hash = 0;
            for (i = 0; i < limit; i++)
            {
                Iterate(ref pages, ref prev, ref next);
                hash = pages[prev].CreateHash();
                if (lookup.ContainsKey(hash))
                    break;

                var answer = GetAnswer(ref pages[prev]);
                answers.Add(answer);
                lookup[hash] = answer;
            }

            var loopStartIdx = answers.IndexOf(lookup[hash]);
            var loopSize = answers.Count - loopStartIdx;
            var diff = limit - i;
            var offset = diff % loopSize;
            var idx = loopStartIdx + offset - 1;

            return answers[idx];
        }
    }
}
