using System.Collections.Generic;

namespace AdventOfCode.Solutions.Days
{
    public class Day10B : Day10A
    {
        public override string Solve()
        {
            var particles = GetParticles();
            return GetNumberOfSeconds(particles).ToString();
        }

        private static int GetNumberOfSeconds(List<Particle> particles)
        {
            var minWidth = float.MaxValue;
            var minHeight = float.MaxValue;
            var secondsPast = 0;
            while (true)
            {
                particles.ForEach(p => p.Tick());
                secondsPast++;

                var stats = GetDimensions(particles);

                if (stats.Width < minWidth)
                    minWidth = stats.Width;
                else break;

                if (stats.Height < minHeight)
                    minHeight = stats.Height;
                else break;
            }

            return secondsPast - 1;
        }
    }
}
