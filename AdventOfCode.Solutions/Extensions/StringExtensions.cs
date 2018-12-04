using System;
using System.Text;

namespace AdventOfCode.Solutions.Extensions
{
    public static class StringExtensions
    {
        public static int[] ParseDelimitedInts(this string line, params char[] delimiters)
        {
            return Array.ConvertAll(
                line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries), 
                int.Parse);
        }

        public static string[] SplitAndRemoveEmpty(this string line)
        {
            return line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string FromByteToBinaryString(this string data)
        {
            var sb = new StringBuilder();

            foreach (var c in data)
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static string FromHexStringToBinaryString(this string data)
        {
            var sb = new StringBuilder();

            foreach (var c in data)
            {
                if(char.IsLetter(c))
                    sb.Append(Convert.ToString((c  - 'a' + 10), 2).PadLeft(4, '0'));
                else if (char.IsNumber(c))
                    sb.Append(Convert.ToString((c - '0'), 2).PadLeft(4, '0'));
            }
            return sb.ToString();
        }
    }
}
