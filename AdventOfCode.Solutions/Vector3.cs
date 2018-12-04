using System;

namespace AdventOfCode.Solutions
{
    public class Vector3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(int[] coords)
        {
            X = coords[0];
            Y = coords[1];
            Z = coords[2];
        }

        public int GetDistanceTo(Vector3 v)
        {
            return Math.Abs(X - v.X) + Math.Abs(Y - v.Y) + Math.Abs(Z - v.Z);
        }
    }
}