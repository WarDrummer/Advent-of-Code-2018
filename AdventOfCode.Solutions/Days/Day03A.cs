using System.Collections.Generic;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day3A : IProblem
    {
        private readonly ParserType _parser;
        protected int MinX = int.MaxValue;
        protected int MinY = int.MaxValue;
        protected int MaxX = int.MinValue;
        protected int MaxY = int.MinValue;

        public Day3A(ParserType parser) { _parser = parser; }

        public Day3A() : this(new ParserType("day03.in")) { }
        
        public virtual string Solve()
        {
            var claims = ParseClaims();

            var fabric = new Fabric(MinX, MaxX, MinY, MaxY);
            foreach (var claim in claims)
            {
                claim.Mark(fabric);
            }

            return fabric.GetOverclaimed().ToString();
        }

        protected IList<FabricClaim> ParseClaims()
        {
            var claims = new List<FabricClaim>();
            foreach (var l in _parser.GetData())
            {
                // #2 @ 675,133: 15x26
                var parts = l.Split(' ');

                var id = parts[0].Substring(1);
                var origin = parts[2].Split(',');
                var fromLeft = int.Parse(origin[0]);
                var fromTop = int.Parse(origin[1].Substring(0, origin[1].Length - 1));
                var size = parts[3].Split('x');
                var width = int.Parse(size[0]);
                var height = int.Parse(size[1]);

                var claim = new FabricClaim(id, fromLeft, fromTop, width, height);
                claims.Add(claim);

                if (fromLeft < MinX) MinX = fromLeft;
                if (fromTop < MinY) MinY = fromTop;
                if (claim.Right > MaxX) MaxX = claim.Right;
                if (claim.Top > MaxY) MaxY = claim.Top;
            }

            return claims;
        }
    }

    public class Fabric
    {
        private readonly Dictionary<int, Dictionary<int, int>> _count;

        public Fabric(int minX, int maxX, int minY, int maxY)
        {
            _count = new Dictionary<int, Dictionary<int, int>>(maxX - minX);
            for (var x = minX; x <= maxX; x++)
            {
                _count[x] = new Dictionary<int, int>(maxY - minY);
                for (var y = minY; y <= maxY; y++)
                {
                    _count[x][y] = 0;
                }
            }
        }

        public void Mark(int x, int y)
        {
            _count[x][y]++;
        }

        public int GetOverclaimed()
        {
            var total = 0;
            foreach (var x in _count.Keys)
            {
                foreach (var y in _count[x].Keys)
                {
                    if (_count[x][y] > 1)
                        total++;
                }
            }

            return total;
        }
    }

    public class FabricClaim
    {
        public string Id { get; }
        public int Left { get; }
        public int Bottom { get; }
        public int Right { get; }
        public int Top { get; }

        public FabricClaim(string id, int left, int bottom, int width, int height)
        {
            Id = id;
            Left = left; 
            Bottom = bottom;
            Right = width + Left - 1;
            Top = height + Bottom - 1;
        }

        public void Mark(Fabric fabric)
        {
            for (var x = Left; x <= Right; x++)
                for (var y = Bottom; y <= Top; y++)
                    fabric.Mark(x, y);
        }

        public bool Overlaps(FabricClaim claim)
        {
            return claim.Left <= Right && 
                   Left <= claim.Right &&
                   claim.Bottom <= Top && 
                   Bottom <= claim.Top;
        }
    }
}
