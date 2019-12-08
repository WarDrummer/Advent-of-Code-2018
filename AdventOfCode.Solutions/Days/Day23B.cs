using AdventOfCode.Solutions.Problem;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day23B : Day23A
    {
        public override string Solve()
        {
            var lines = Parser.GetData().ToList();
            var bots = new List<Nanobot>();

            foreach (var l in lines)
            {
                var bot = Nanobot.Create(l);
                bots.Add(bot);
            }

            var counts = new Dictionary<Nanobot, int>();

            for(var i = 0; i < bots.Count; i++)
            {
                for (var j = i+1; j < bots.Count; j++)
                {
                    
                    if (bots[i].InRange(bots[j]))
                    {
                        if (!counts.ContainsKey(bots[i]))
                            counts[bots[i]] = 0;

                        if (!counts.ContainsKey(bots[j]))
                            counts[bots[j]] = 0;

                        counts[bots[i]]++;
                        counts[bots[j]]++;
                    }
                }
            }

            var distance = int.MaxValue;
            var max = counts.Values.Max();
            var origin = new Point3(0, 0, 0);
            foreach(var kvp in counts)
            {
                if(kvp.Value == max)
                {
                    var d = origin.DistanceTo(kvp.Key.Location);
                    if(d < distance)
                    {
                        distance = d;
                    }
                }
            }

            return distance.ToString();
        }
    }
}
