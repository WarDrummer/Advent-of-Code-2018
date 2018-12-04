using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{
    internal class SubsetSum
    {
        public static IEnumerable<string> GetCombinations(int[] set, int sum, string values)
        {
            for (var i = 0; i < set.Length; i++)
            {
                var left = sum - set[i];
                var vals = set[i] + "," + values;
                if (left == 0)
                {
                    yield return vals;
                }
                else
                {
                    var possible = set.Take(i).Where(n => n <= sum).ToArray();
                    if (possible.Length > 0)
                    {
                        foreach (var s in GetCombinations(possible, left, vals))
                        {
                            yield return s;
                        }
                    }
                }
            }
        }

    }
}
