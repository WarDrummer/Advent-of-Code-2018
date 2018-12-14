using System;

namespace AdventOfCode.Solutions.Coordinates
{
    public struct Coordinate : IComparable<Coordinate>, IEquatable<Coordinate>
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int CompareTo(Coordinate b)
        {
            if ((X == b.X) && (Y == b.Y))
                return 0;

            if ((X < b.X) || ((X == b.X) && (Y < b.Y)))
                return -1;

            return 1;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public int DistanceTo(Coordinate c)
        {
            return Math.Abs(X - c.X) + Math.Abs(Y - c.Y);
        }

        public bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Coordinate && Equals((Coordinate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}
