using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day10A : IProblem
    {
        protected readonly ParserType Parser;

        public Day10A(ParserType parser) { Parser = parser; }

        public Day10A() : this(new ParserType("day10.in")) { }

        public virtual string Solve()
        {
            var particles = GetParticles();
            particles.ForEach(p => p.Advance(10710));

            var stats = GetDimensions(particles);
            SaveImage(stats, particles, "Day10_Message.bmp");

            return "ZRABXXJC";
        }

        private const int Padding = 100;
        private const int PaddingOffset = Padding / 2;
        private const int ScaleFactor = 5;

        private static void SaveImage(PointCloudStats stats, IEnumerable<Particle> particles, string filename)
        {
            var width = (int) (stats.Width + 1);
            var height = (int) (stats.Height + 1);
            using (var bitmap = new Bitmap(width * ScaleFactor + Padding, height * ScaleFactor + Padding))
            {
                var graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);
                foreach (var p in particles)
                {
                    var x = (p.Position.X - stats.MinX) * ScaleFactor + PaddingOffset;
                    var y = (p.Position.Y - stats.MinY) * ScaleFactor + PaddingOffset;
                    graphics.FillRectangle(Brushes.Black, x, y, ScaleFactor, ScaleFactor);
                }

                bitmap.Save(filename);
            }
        }

        protected List<Particle> GetParticles()
        {
            var particles = new List<Particle>();
            foreach (var line in Parser.GetData())
            {
                var startPosition = line.IndexOf('<') + 1;
                var endPosition = line.IndexOf('>');
                var position = ExtractVector(
                    line.Substring(
                        startPosition, 
                        endPosition - startPosition));

                var startVelocity = line.LastIndexOf('<') + 1;
                var endVelocity = line.LastIndexOf('>');
                var velocity = ExtractVector(
                    line.Substring(
                        startVelocity,
                        endVelocity - startVelocity));

                particles.Add(new Particle(position, velocity));
            }

            return particles;
        }

        private static Vector3 ExtractVector(string coordinates)
        {
            var xy = coordinates
                .Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            return new Vector3(xy[0], xy[1], 0);
        }

        protected static PointCloudStats GetDimensions(IEnumerable<Particle> particles)
        {
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            var maxX = float.MinValue;
            var maxY = float.MinValue;

            foreach (var p in particles)
            {
                if (p.Position.X < minX)
                    minX = p.Position.X;
                if (p.Position.X > maxX)
                    maxX = p.Position.X;
                if (p.Position.Y < minY)
                    minY = p.Position.Y;
                if (p.Position.Y > maxY)
                    maxY = p.Position.Y;
            }

            var width = maxX - minX;
            var height = maxY - minY;

            return new PointCloudStats(width, height, minX, minY);
        }

        public struct PointCloudStats
        {
            public float Width { get; }
            public float Height { get; }
            public float MinX { get; }
            public float MinY { get; }

            public PointCloudStats(float width, float height, float minX, float minY)
            {
                Width = width;
                Height = height;
                MinX = minX;
                MinY = minY;
            }
        }
    }
}
