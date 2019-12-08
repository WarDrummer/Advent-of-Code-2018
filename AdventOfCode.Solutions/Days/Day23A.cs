using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day23A : IProblem
    {
        protected readonly ParserType Parser;

        public Day23A(ParserType parser) { Parser = parser; }

        public Day23A() : this(new ParserType("day23.in")) { }

        public virtual string Solve()
        {
            var lines = Parser.GetData().ToList();
            var bots = new List<Nanobot>();

            Nanobot mostPowerful = null;
            foreach(var l in lines)
            {
                var bot = Nanobot.Create(l);
                bots.Add(bot);

                if (mostPowerful == null || mostPowerful.Radius < bot.Radius)
                    mostPowerful = bot;
            }

            var count = 0;
            foreach(var bot in bots)
            {
                if (bot.Location.DistanceTo(mostPowerful.Location) <= mostPowerful.Radius)
                {
                    count++;
                }
            }
            return count.ToString();
        }

        public class Nanobot
        {
            public Nanobot(int radius, Point3 location)
            {
                Radius = radius;
                Location = location;
            }

            public static Nanobot Create(string s)
            {
                // pos=<0,0,0>, r=4
                var r = int.Parse(s.Substring(s.IndexOf("r=") + 2));
                var start = s.IndexOf('<') + 1;
                var len = s.IndexOf('>') - start;
                var xyz = s.Substring(start, len).Split(',').Select(int.Parse).ToArray();
                return new Nanobot(r, new Point3(xyz[0], xyz[1], xyz[2]));
            }

            public int Radius { get; }
            public Point3 Location { get; }

            public bool InRange(Nanobot n)
            {
                return Location.DistanceTo(n.Location) <= Math.Max(n.Radius, Radius);
            }
        }

        public struct Point3
        {
            public Point3(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public int X { get; }
            public int Y { get; }
            public int Z { get; }

            public int DistanceTo(Point3 p)
            {
                return Math.Abs(p.X - X) + Math.Abs(p.Y - Y) + Math.Abs(p.Z - Z);
            }

            public override string ToString()
            {
                return $"";
            }
        }
    }
}
