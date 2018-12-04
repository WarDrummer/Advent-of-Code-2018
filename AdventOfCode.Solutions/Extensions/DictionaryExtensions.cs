using System.Collections.Generic;

namespace AdventOfCode.Solutions.Extensions
{
    internal static class DictionaryExtensions
    {
        internal static T GetValueOrDefault<T,U>(this IDictionary<U,T> dictionary, U key, T defaultValue)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : defaultValue;
        }
    }
}
