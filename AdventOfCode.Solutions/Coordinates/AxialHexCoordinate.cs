using System;

namespace AdventOfCode.Solutions.Coordinates
{
    public class AxialHexCoordinate
    {
        public int Q { get; }
        public int R { get; }

        public AxialHexCoordinate(int q, int r)
        {
            Q = q;
            R = r;
        }

        public CubeHexCoordinate ToCubeHex()
        {
            var x = Q;
            var z = R;
            var y = -x - z;
            return new CubeHexCoordinate(x, y, z);
        }

        public int GetDistanceTo(AxialHexCoordinate other)
        {
            var cubeCoordinates = ToCubeHex();
            var otherCubeCoordinates = other.ToCubeHex();

            return Math.Max(
                Math.Max(
                    Math.Abs(cubeCoordinates.X - otherCubeCoordinates.X), 
                    Math.Abs(cubeCoordinates.Y - otherCubeCoordinates.Y)),
                Math.Abs(cubeCoordinates.Z - otherCubeCoordinates.Z));
        }
    }
}