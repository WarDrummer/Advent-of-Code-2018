using System.Collections.Generic;
using System.Text;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day5A : IProblem
    {
        private readonly ParserType _parser;

        public Day5A(ParserType parser) { _parser = parser; }

        public Day5A() : this(new ParserType("day05.in")) { }

        public virtual string Solve()
        {
            var input = _parser.GetData();
            var reversePolarity = CreatePolarityLookup();
            input = ReactPolymerFast(input, reversePolarity);
            return input.Length.ToString();
        }

        protected static Dictionary<char, char> CreatePolarityLookup()
        {
            var reversePolarity = new Dictionary<char, char>();
            for (char lower = 'a', upper = 'A'; lower <= 'z'; lower++, upper++)
            {
                reversePolarity.Add(lower, upper);
                reversePolarity.Add(upper, lower);
            }

            return reversePolarity;
        }

        protected static string ReactPolymerFast(string input, IReadOnlyDictionary<char, char> reversePolarity)
        {
            var head = input.BuildLinkedList();
            bool removed;
            do
            {
                removed = false;
                var node = head;
                var prev = node;

                while (node.Next != null)
                {
                    if (node.Value == reversePolarity[node.Next.Value])
                    {
                        removed = true;

                        if (node.Next?.Next != null)
                        {
                            node.Value = node.Next.Next.Value;
                            node.Next = node.Next.Next.Next;
                        }
                        else
                        {
                            node = prev;
                            node.Next = null;
                        }
                    }
                    else
                    {
                        prev = node;
                        node = node.Next;
                    }
                }
            } while (removed);

            return head.ToString();
        }

        protected static string ReactPolymer(string input, IReadOnlyDictionary<char, char> reversePolarity)
        {
            var sb = new StringBuilder(input.Length);
            int currentLength;
            do
            {
                sb.Clear();
                int i;
                var lastCharIndex = input.Length - 1;
                for (i = 0; i < lastCharIndex; i++)
                {
                    var c1 = input[i];
                    if (c1 == reversePolarity[input[i + 1]])
                    {
                        i++;
                        continue;
                    }

                    sb.Append(c1);
                }

                if (i == lastCharIndex)
                    sb.Append(input[lastCharIndex]);

                currentLength = input.Length;
                input = sb.ToString();
            } while (sb.Length < currentLength);

            return input;
        }
    }
}
