using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Solutions.Coordinates;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day17A : IProblem
    {
        protected readonly ParserType Parser;

        public Day17A(ParserType parser) { Parser = parser; }

        public Day17A() : this(new ParserType("day17.in")) { }

        public virtual string Solve()
        {
            var results = ParseInput();
            var clay = results.Item1;
            var boundingBox = results.Item2;

            var tracer = new WaterTracer(clay, boundingBox);
            tracer.Compute();
            tracer.CountWaterTiles();
            return tracer.TotalWaterCount.ToString();
        }

        protected Tuple<HashSet<Coordinate>, BoundingBox> ParseInput()
        {
            int minX = int.MaxValue, maxX =int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
            var clay = new HashSet<Coordinate>();
            var lines = Parser.GetData();
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ", ", "=", ".." }, StringSplitOptions.RemoveEmptyEntries);

                if (parts[0] == "x")
                {
                    var x = int.Parse(parts[1]);
                    var y1 = int.Parse(parts[3]);
                    var y2 = int.Parse(parts[4]);
                    for (var y = y1; y <= y2; y++)
                        clay.Add(new Coordinate(x, y));

                    minX = Math.Min(x, minX);
                    maxX = Math.Max(x, maxX);
                    minY = Math.Min(y2, Math.Min(y1, minY));
                    maxY = Math.Max(y2, Math.Max(y1, maxY));
                }
                else if (parts[0] == "y")
                {
                    var y = int.Parse(parts[1]);
                    var x1 = int.Parse(parts[3]);
                    var x2 = int.Parse(parts[4]);
                    for (var x = x1; x <= x2; x++)
                        clay.Add(new Coordinate(x, y));

                    minY = Math.Min(y, minY);
                    maxY = Math.Max(y, maxY);
                    minX = Math.Min(x2, Math.Min(x1, minX));
                    maxX = Math.Max(x2, Math.Max(x1, maxX));
                }
            }
            return new Tuple<HashSet<Coordinate>, BoundingBox>(clay, new BoundingBox(minX - 5, maxX + 5, minY, maxY));
        }
    }

    public class WaterTracer
    {
        private readonly HashSet<Coordinate> _clay;
        private readonly BoundingBox _boundingBox;
        private readonly HashSet<Coordinate> _seenStreams = new HashSet<Coordinate>();
        public HashSet<Coordinate> PoolingWater { get; } = new HashSet<Coordinate>();
        public HashSet<Coordinate> FlowingWater { get; } = new HashSet<Coordinate>();

        public WaterTracer(HashSet<Coordinate> clay, BoundingBox boundingBox)
        {
            _clay = clay;
            _boundingBox = boundingBox;
        }

        public void Compute()
        {
            var streamOrigins = new List<Coordinate> { new Coordinate(500, _boundingBox.Bottom) };
            while (streamOrigins.Count > 0)
            {
                var newStreams = new List<Coordinate>();
                foreach (var streamOrigin in streamOrigins)
                {
                    var hit = GetNextHit(streamOrigin);
                    if (hit != null)
                    {
                        var hitValue = hit.Value;
                        hitValue.Y--;

                        var xRight = GetRightBound(hitValue);
                        var xLeft = GetLeftBound(hitValue);

                        if (xRight == null || xLeft == null)
                            continue;

                        var leftHitBound = xLeft.Value.X;
                        var rightHitBound = xRight.Value.X;

                        while (true)
                        {
                            var y = hitValue.Y;
                            if (xLeft != null && xRight != null &&
                                leftHitBound <= xLeft.Value.X && rightHitBound >= xRight.Value.X)
                            {
                                for (var x = xLeft.Value.X; x <= xRight.Value.X; x++)
                                    PoolingWater.Add(new Coordinate(x, y));
                            }
                            else
                            {

                                // hits left boundary of current bucket
                                if (xLeft != null && leftHitBound > xLeft.Value.X)
                                {
                                    FlowingWater.Add(new Coordinate(leftHitBound - 1, y));
                                    AddStream(newStreams, new Coordinate(leftHitBound - 2, y));
                                }

                                // no boundary to left
                                if (xLeft == null)
                                {
                                    FlowingWater.Add(new Coordinate(leftHitBound - 1, y));
                                    AddStream(newStreams, new Coordinate(leftHitBound - 2, y));
                                }

                                // hits right boundary of current bucket
                                if (xRight != null && rightHitBound < xRight.Value.X)
                                {
                                    FlowingWater.Add(new Coordinate(rightHitBound + 1, y));
                                    AddStream(newStreams, new Coordinate(rightHitBound + 2, y));
                                }

                                // no boundary to right
                                if (xRight == null)
                                {
                                    FlowingWater.Add(new Coordinate(rightHitBound + 1, y));
                                    AddStream(newStreams, new Coordinate(rightHitBound + 2, y));
                                }

                                // Add flowing water across top
                                hitValue.Y++;

                                xRight = GetRightBound(hitValue);
                                xLeft = GetLeftBound(hitValue);

                                if (xLeft != null && xRight != null)
                                {
                                    for (var x = xLeft.Value.X; x <= xRight.Value.X; x++)
                                    {
                                        FlowingWater.Add(new Coordinate(x, hitValue.Y - 1));
                                        AddStream(newStreams, new Coordinate(x, hitValue.Y - 1));
                                    }
                                }

                                break;
                            }

                            hitValue.Y--;
                            xRight = GetRightBound(hitValue);
                            xLeft = GetLeftBound(hitValue);
                        }
                    }
                    else
                    {
                        newStreams.Remove(streamOrigin);
                    }
                }

                streamOrigins = newStreams;
            }
        }

        private Coordinate? GetLeftBound(Coordinate origin)
        {
            var y = origin.Y;
            for (var x = origin.X; x >= _boundingBox.Left; x--)
                if (_clay.Contains(new Coordinate(x, y)))
                    return new Coordinate(x + 1, y);

            return null;
        }

        private Coordinate? GetRightBound(Coordinate origin)
        {
            var y = origin.Y;
            for (var x = origin.X; x <= _boundingBox.Right; x++)
                if (_clay.Contains(new Coordinate(x, y)))
                    return new Coordinate(x - 1, y);

            return null;
        }

        private Coordinate? GetNextHit(Coordinate streamOrigin)
        {
            var x = streamOrigin.X;
            for (var y = streamOrigin.Y; y <= _boundingBox.Top; y++)
            {
                if (_clay.Contains(new Coordinate(x, y)))
                    return new Coordinate(x, y);

                FlowingWater.Add(new Coordinate(x, y));
            }

            return null;
        }

        private void AddStream(ICollection<Coordinate> newStreams, Coordinate waterSource)
        {
            if (!_seenStreams.Contains(waterSource))
            {
                _seenStreams.Add(waterSource);
                newStreams.Add(waterSource);
            }
        }

        public int PooledWaterCount { get; private set; }
        public int FlowingWaterCount { get; private set; }
        public int TotalWaterCount => PooledWaterCount + FlowingWaterCount;

        public void CountWaterTiles()
        {
            FlowingWaterCount = 0;
            PooledWaterCount = 0;
            for (var y = _boundingBox.Bottom; y <= _boundingBox.Top; y++)
            {
                for (var x = _boundingBox.Left; x <= _boundingBox.Right; x++)
                {
                    if (y == 0 && x == 500)
                        continue;
                    if (PoolingWater.Contains(new Coordinate(x, y)))
                        PooledWaterCount++;
                    else if (FlowingWater.Contains(new Coordinate(x, y)))
                        FlowingWaterCount++;
                }
            }
        }

        public void Print()
        {
            using (var file = new StreamWriter(@"C:\katas\day17A.txt"))
            {
                for (var y = _boundingBox.Bottom; y <= _boundingBox.Top; y++)
                {
                    for (var x = _boundingBox.Left; x <= _boundingBox.Right; x++)
                    {
                        if (y == 0 && x == 500)
                            file.Write("x");
                        else if (PoolingWater.Contains(new Coordinate(x, y)) && !_clay.Contains(new Coordinate(x, y)))
                            file.Write("~");
                        else if (FlowingWater.Contains(new Coordinate(x, y)) && !_clay.Contains(new Coordinate(x, y)))
                            file.Write("|");
                        file.Write(_clay.Contains(new Coordinate(x, y)) ? "#" : ".");
                    }

                    file.WriteLine();
                }
            }
        }
    }
}
