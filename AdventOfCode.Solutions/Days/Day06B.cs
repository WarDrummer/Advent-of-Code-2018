using AdventOfCode.Solutions.Coordinates;

namespace AdventOfCode.Solutions.Days
{
    public class Day6B : Day6A
    {
        public override string Solve()
        {
            var points = GetPoints();
            var boundingBox = GetBoundingBox(points);
            return GetRegionCount(boundingBox, points).ToString();
        }

        private static int GetRegionCount(BoundingBox boundingBox, Coordinate[] points)
        {
            var count = 0;
            foreach (var coordinate in boundingBox.GetCoordinates())
            {
                var totalDistance = 0;
                foreach (var pt in points)
                    totalDistance += pt.DistanceTo(coordinate);

                if(totalDistance < 10000)
                    count++;
            }

            return count;
        }
    }
}
