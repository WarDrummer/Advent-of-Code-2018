using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day18A : IProblem
    {
        protected readonly ParserType Parser;

        public Day18A(ParserType parser) { Parser = parser; }

        public Day18A() : this(new ParserType("day18.in")) { }

        public const char OPEN = '.';
        public const char TREES = '|';
        public const char LUMBERYARD = '#';

        public virtual string Solve()
        {
            var one = Parser.GetData().Select(s => s.ToArray()).ToArray();
            var two = Parser.GetData().Select(s => s.ToArray()).ToArray();

            char[][][] pages = { one, two };
            var prev = 0;
            var next = 1;

            for (var i = 0; i < 10; i++)
            {
                Iterate(ref pages, ref prev, ref next);
            }

            return GetAnswer(ref pages[prev]);
        }

        protected static void Iterate(ref char[][][] pages, ref int prev, ref int next)
        {
            int tmp = 0;
            var p = pages[prev];
            var n = pages[next];

            for (var y = 0; y < p.Length; y++)
            {
                for (var x = 0; x < p[0].Length; x++)
                {
                    switch (p[y][x])
                    {
                        case OPEN:
                            if (GetAdjacentCount(ref p, x, y, TREES) >= 3)
                                n[y][x] = TREES;
                            else n[y][x] = p[y][x];

                            break;
                        case TREES:
                            if (GetAdjacentCount(ref p, x, y, LUMBERYARD) >= 3)
                                n[y][x] = LUMBERYARD;
                            else n[y][x] = p[y][x];
                            break;
                        case LUMBERYARD:
                            if (GetAdjacentCount(ref p, x, y, LUMBERYARD) > 0 && GetAdjacentCount(ref p, x, y, TREES) > 0)
                                n[y][x] = LUMBERYARD;
                            else
                                n[y][x] = OPEN;
                            break;
                        default:
                            throw new Exception($"Unrecognized symbol: {p[y][x]}");
                    }
                }
            }

            tmp = prev;
            prev = next;
            next = tmp;
        }

        protected static string GetAnswer(ref char[][] result)
        {
            var treeCount = 0;
            var lumberYardCount = 0;
            for (var y = 0; y < result.Length; y++)
            {
                for (var x = 0; x < result[0].Length; x++)
                {
                    if (result[y][x] == TREES)
                        treeCount++;
                    else if (result[y][x] == LUMBERYARD)
                        lumberYardCount++;
                }
            }
            return (treeCount * lumberYardCount).ToString();
        }

        protected static int GetAdjacentCount(ref char[][] map, int x, int y, char symbol)
        {
            var yMax = map.Length;
            var xMax = map[0].Length;
            var count = 0;

            if (x-1 >= 0 && map[y][x-1] == symbol)
                count++;

            if (x-1 >= 0 && y-1 >=0 && map[y-1][x-1] == symbol)
                count++;

            if (x - 1 >= 0 && y + 1 < yMax && map[y + 1][x - 1] == symbol)
                count++;

            if (x + 1 < xMax && map[y][x + 1] == symbol)
                count++;

            if (x + 1 < xMax && y - 1 >= 0 && map[y - 1][x + 1] == symbol)
                count++;

            if (x + 1 < xMax && y + 1 < yMax && map[y + 1][x + 1] == symbol)
                count++;

            if (y - 1 >= 0 && map[y-1][x] == symbol)
                count++;

            if (y + 1 < yMax && map[y + 1][x] == symbol)
                count++;

            return count;
        }
    }
}
