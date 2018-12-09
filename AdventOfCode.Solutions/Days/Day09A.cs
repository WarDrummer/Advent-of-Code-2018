using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;
    using Marble = DoublyLinkedNode<int>;

    public class Day9A : IProblem
    {
        private readonly ParserType _parser;

        public Day9A(ParserType parser) { _parser = parser; }

        public Day9A() : this(new ParserType("day09.in")) { }
        
        protected int EndMarble { get; set; } = 71339;

        public virtual string Solve()
        {
            return GetHighScore(418, EndMarble);
        }

        protected static string GetHighScore(int numberOfPlayers, int endMarbleValue)
        {
            var nextMarble = 1;
            var current = new Marble(0);
            var currentPlayer = 0;
            var scores = new long[numberOfPlayers];

            while (nextMarble < endMarbleValue)
            {
                if (nextMarble % 23 != 0)
                {
                    if (current.Next != null)
                        current = current.Next;
                    current = current.Insert(nextMarble);
                }
                else
                {
                    var sevenBack = current.GetPrevious(7);
                    scores[currentPlayer] += nextMarble + sevenBack.Value;
                    sevenBack.Remove();
                    current = sevenBack.Next;
                }

                nextMarble++;
                currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            }

            return scores.Max().ToString();
        }
    }
}
