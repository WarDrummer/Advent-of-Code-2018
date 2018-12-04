using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Extensions
{
    internal static class PermutationExtensions
    {
        public static IEnumerable<IList<int>> GetPermutations(this IList<int> items)
        {
            return GetPermutations<int>(items);
        }

        public static IEnumerable<IList<string>> GetPermutations(this IList<string> items)
        {
            return GetPermutations<string>(items);
        }

        public static IEnumerable<IList<T>> GetPermutations<T>(IList<T> items)
        {
            if (items.Count == 1)
            {
                yield return items;
            }
            else
            {
                for (var i = 0; i < items.Count; i++)
                {
                    foreach (var permutation in GetPermutations(items.Where((value, index) => index != i).ToList()))
                    {
                        permutation.Insert(0, items[i]);
                        yield return permutation;
                    }
                }
            }
        }
    }
}
