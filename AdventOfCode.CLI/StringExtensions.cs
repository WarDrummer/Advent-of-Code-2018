using System;

namespace AdventOfCode
{
    public static class StringExtensions
    {
        public static void ToConsole(this string s, string day = "")
        {
            Console.WriteLine($"{day} result: {s}".Trim());
            Console.WriteLine();
        }
    }
}
