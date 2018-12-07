using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Coordinates;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day6A : IProblem
    {
        protected readonly ParserType FileParser;

        public Day6A(ParserType fileParser) { FileParser = fileParser; }

        public Day6A() : this(new ParserType("day06.in")) { }

        public virtual string Solve()
        {
            var points = GetPoints();
            var boundingBox = GetBoundingBox(points);
            var counts = GetClosestNumberOfPointsCounts(boundingBox, points);
            var max = GetMaxCount(points, boundingBox, counts);
            return max.ToString();
        }

        protected static int GetMaxCount(Coordinate[] points, BoundingBox boundingBox, Dictionary<Coordinate, int> counts)
        {
            var max = 0;
            var finitePoints = GetFinitePoints(points, boundingBox);
            foreach (var finitePoint in finitePoints)
            {
                var cnt = counts[finitePoint];
                if (cnt > max)
                    max = cnt;
            }

            return max;
        }

        protected static Dictionary<Coordinate, int> GetClosestNumberOfPointsCounts(BoundingBox boundingBox, Coordinate[] points)
        {
            var counts = new Dictionary<Coordinate, int>();
            foreach (var coordinate in boundingBox.GetCoordinates())
            {
                var min = int.MaxValue;
                Coordinate? minCoord = null;
                foreach (var point in points)
                {
                    var d = point.DistanceTo(coordinate);
                    if (d < min)
                    {
                        min = d;
                        minCoord = point;
                    }
                    else if (d == min)
                        minCoord = null;
                }

                if (minCoord != null)
                {
                    if (!counts.ContainsKey(minCoord.Value))
                        counts[minCoord.Value] = 0;
                    counts[minCoord.Value]++;
                }
            }

            return counts;
        }

        protected static IEnumerable<Coordinate> GetFinitePoints(Coordinate[] points, BoundingBox boundingBox)
        {
            var infinitePoints = new HashSet<Coordinate>();
            foreach (var boundaryPoint in boundingBox.GetBoundaryPoints())
            {
                var minDistance = int.MaxValue;
                var minPoint = new Coordinate(0,0);
                foreach (var point in points)
                {
                    var distance = point.DistanceTo(boundaryPoint);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minPoint = point;
                    }
                }

                if (!infinitePoints.Contains(minPoint))
                    infinitePoints.Add(minPoint);
            }

            var finitePoints = new List<Coordinate>();
            foreach (var point in points)
            {
                if(!infinitePoints.Contains(point))
                    finitePoints.Add(point);
            }
            return finitePoints;
        }

        protected static BoundingBox GetBoundingBox(Coordinate[] points)
        {
            int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
            foreach (var pt in points)
            {
                if (pt.X < minX)
                    minX = pt.X;
                if (pt.X > maxX)
                    maxX = pt.X;
                if (pt.Y < minY)
                    minY = pt.Y;
                if (pt.Y > maxY)
                    maxY = pt.Y;
            }

            return new BoundingBox(minX, maxX, minY, maxY);
        }

        protected Coordinate[] GetPoints()
        {
            var input = FileParser.GetData();

            var points = input.Select(s =>
            {
                var xy = s.Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries);
                return new Coordinate(int.Parse(xy[0]), int.Parse(xy[1]));
            }).ToArray();
            return points;
        }
    }

    public class BoundingBox
    {
        public int Left { get; }
        public int Right { get; }
        public int Bottom { get; }
        public int Top { get; }

        public BoundingBox(int left, int right, int bottom, int top)
        {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
        }

        public IEnumerable<Coordinate> GetCoordinates()
        {
            for(var x = Left; x <= Right; x++)
                for(var y = Bottom; y <= Top; y++)
                    yield return new Coordinate(x, y);
        }

        public IEnumerable<Coordinate> GetBoundaryPoints()
        {
            for (var x = Left; x <= Right; x++)
            {
                yield return new Coordinate(x, Bottom);
                yield return new Coordinate(x, Top);
            }

            for (var y = Bottom; y <= Top; y++)
            {
                yield return new Coordinate(Left, y);
                yield return new Coordinate(Right, y);
            }
        }
    }
}
