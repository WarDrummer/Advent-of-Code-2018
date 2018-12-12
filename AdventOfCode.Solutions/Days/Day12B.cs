using System.Collections.Generic;

namespace AdventOfCode.Solutions.Days
{
    public class Day12B : Day12A
    {
        public override string Solve()
        {
            padCount = 10;
            var initial = GetInitial();

            var state = new List<char>(initial.ToCharArray());

            var configurations = GetConfigurations();
            var previousCount = 0;
            var previousDiff = 0;
            for (ulong i = 1; i < 50000000000; i++)
            {
                state = Grow(state, configurations);
                var count = GetCount(state);
                var diff = count - previousCount;
                if (diff == previousDiff)
                {
                    var result = (50000000000 - i) * (ulong)diff + (ulong)count;
                    return result.ToString();
                }

                previousDiff = diff;
                previousCount = count;
            }

            return "Failed";
        }
    }
}
