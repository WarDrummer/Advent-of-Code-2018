using System;

namespace AdventOfCode.Solutions.Coordinates
{
    public struct Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public int DistanceTo(Coordinate c)
        {
            return Math.Abs(X - c.X) + Math.Abs(Y - c.Y);
        }
    }
}
