using System;

namespace AdventOfCode.Solutions
{
    public class ZeroVector3 : Vector3
    {
        public override float X { get => 0; set { } }
        public override float Y { get => 0; set { } }
        public override float Z { get => 0; set { } }

        public ZeroVector3() : base(0, 0, 0) { }
    }

    public class Vector3
    {
        // ReSharper disable once InconsistentNaming
        private static readonly ZeroVector3 _zero = new ZeroVector3();
        public static Vector3 Zero => _zero;

        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public virtual float Z { get; set; }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(float[] coords)
        {
            X = coords[0];
            Y = coords[1];
            Z = coords[2];
        }

        public float GetDistanceTo(Vector3 v)
        {
            return Math.Abs(X - v.X) + Math.Abs(Y - v.Y) + Math.Abs(Z - v.Z);
        }
    }
}