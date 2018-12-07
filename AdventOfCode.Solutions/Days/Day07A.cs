using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day7A : IProblem
    {
        private readonly ParserType _parser;

        public Day7A(ParserType parser) { _parser = parser; }

        public Day7A() : this(new ParserType("day07.in")) { }

        public virtual string Solve()
        {
            var requirements = new Dictionary<char, List<char>>();
            var prereqs = new Dictionary<char, List<char>>();

            foreach (var instruction in _parser.GetData())
            {
                var prereq = instruction[5];
                var step = instruction[36];

                if (!requirements.ContainsKey(step))
                    requirements.Add(step, new List<char>());
                requirements[step].Add(prereq);

                if (!prereqs.ContainsKey(prereq))
                    prereqs.Add(prereq, new List<char>());
                prereqs[prereq].Add(step);
            }

            var stepsToAdd = new List<char>();
            var steps = prereqs.Keys.ToArray();
            foreach (var step in steps)
            {
                if (requirements[step].Count < 1)
                {
                    stepsToAdd.Add(step);
                    requirements.Remove(step);
                }
            }

            foreach (var step in stepsToAdd)
            {
                prereqs.Remove(step);
            }


            // get steps without prereqs

            // sort alphabetically
            // add to result
            // remove from dictionary

            //var prereqs = new List<char>();
            //foreach (var instruction in _parser.GetData())
            //{
            //    var prereq = instruction[5];
            //    var step = instruction[36];
            //    if (!requirements.ContainsKey(step))
            //        requirements.Add(step, new List<char>());
            //    if(!prereqs.Contains(prereq))
            //        prereqs.Add(prereq);

            //    requirements[step].Add(prereq);
            //}

            //foreach (var step in requirements.Keys)
            //    prereqs.Remove(step);

            //var sb = new StringBuilder();
            //sb.Append(prereqs[0]);

            //Console.WriteLine(prereqs[0]);

            return "Unknown";
        }
    }

}
