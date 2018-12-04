using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Solutions
{
    internal class KnotHasher
    {
        public static string ComputeKnotHash(string data)
        {
            var lengths = data.Select(c => (int) c).ToList();
            lengths.AddRange(new[] {17, 31, 73, 47, 23});

            var head = Enumerable.Range(0, 256).ToArray().BuildCircularList();

            var skipSize = 0;
            var current = head;

            for (var count = 0; count < 64; count++)
                KnotHash(lengths, ref current, ref skipSize);

            var denseHash = ConvertToDenseHash(head);
            return ConvertToHashString(denseHash);
        }

        public static void KnotHash(List<int> lengths, ref Node<int> node, ref int skipSize)
        {
            var stack = new Stack<int>();
            foreach (var length in lengths)
            {
                var current = node;

                for (var lengthCount = 0; lengthCount < length; lengthCount++)
                {
                    stack.Push(node.Value);
                    node = node.Next;
                }

                while (stack.Count > 0)
                {
                    current.Value = stack.Pop();
                    current = current.Next;
                }

                for (var advanceCount = 0; advanceCount < skipSize; advanceCount++)
                    node = node.Next;

                skipSize++;
            }
        }

        private static string ConvertToHashString(IEnumerable<int> hash)
        {
            var sb = new StringBuilder();

            foreach (var hex in hash)
                sb.Append(hex.ToString("x2"));

            return sb.ToString();
        }

        private static int[] ConvertToDenseHash(Node<int> head)
        {
            var output = new int[16];
            var values = head.ToList();

            for (var x = 0; x < values.Count; x += 16)
            {
                output[x / 16] =
                    values[x] ^ values[x + 1] ^ values[x + 2] ^ values[x + 3] ^
                    values[x + 4] ^ values[x + 5] ^ values[x + 6] ^ values[x + 7] ^
                    values[x + 8] ^ values[x + 9] ^ values[x + 10] ^ values[x + 11] ^
                    values[x + 12] ^ values[x + 13] ^ values[x + 14] ^ values[x + 15];
            }
            return output;
        }
    }
}