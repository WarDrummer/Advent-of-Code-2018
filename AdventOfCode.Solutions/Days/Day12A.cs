using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day12A : IProblem
    {
        protected readonly ParserType Parser;

        public Day12A(ParserType parser) { Parser = parser; }

        public Day12A() : this(new ParserType("day12.in")) { }
        protected static int padCount = 10;
        public virtual string Solve()
        {
            padCount = 10;

            var initial = GetInitial();
            var state = new List<char>(initial.ToCharArray());
            var configurations = GetConfigurations();

            for (long i = 0; i < 20; i++)
                state = Grow(state, configurations);
            return GetCount(state).ToString();
        }

        protected static string GetInitial()
        {
            var initial =
                "##..#.#.#..##..#..##..##..#.#....#.....##.#########...#.#..#..#....#.###.###....#..........###.#.#..";
            var padding = new string('.', padCount);
            initial = $"{padding}{initial}{padding}";
            return initial;
        }

        protected Dictionary<string, char> GetConfigurations()
        {
            var configurations = new Dictionary<string, char>();
            var lines = Parser.GetData();
            foreach (var line in lines)
            {
                var parts = line.Split(new[] {" => "}, StringSplitOptions.RemoveEmptyEntries);
                configurations.Add(parts[0], parts[1][0]);
            }

            return configurations;
        }

        protected static List<char> Grow(List<char> state, Dictionary<string, char> configurations)
        {
            var newState = new List<char>(state.Count);
            foreach (var segment in GetSegments(state))
            {
                var matcher = segment.Item1;
                if (configurations.ContainsKey(matcher))
                    newState.Add(configurations[segment.Item1]);
                else
                    newState.Add('.');
            }

            if (newState.Last() == '#')
                newState.Add('.');
            newState.Insert(0, '.');
            padCount++;

            return newState;
        }

        protected static int GetCount(List<char> state)
        {
            var total = 0;
            for (var i = 0; i < state.Count; i++)
                if (state[i] == '#')
                    total += i - padCount;
            return total;
        }

        private static IEnumerable<Tuple<string, int>> GetSegments(IReadOnlyList<char> state)
        {
            var q = new Queue<char>(new [] {'.', '.', '.', state[0], state[1]});
            int idx;
            for (idx = 2; idx < state.Count; idx++)
            {
                q.Dequeue();
                q.Enqueue(state[idx]);
                yield return new Tuple<string, int>(new string(q.ToArray()), idx - 2);
            }

            for (; idx < state.Count + 2; idx++)
            {
                q.Dequeue();
                q.Enqueue('.');
                yield return new Tuple<string, int>(new string(q.ToArray()), idx - 2);
            }
        }
    }
}
