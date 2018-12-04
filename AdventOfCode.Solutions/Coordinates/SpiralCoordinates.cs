using System.Collections.Generic;

namespace AdventOfCode.Solutions.Coordinates
{
    internal static class SpiralCoordinates
    {
        internal static IEnumerable<Coordinate> GenerateCounterClockwise(int number)
        {
            var count = 1;
            var x = 1;
            var y = 1;
            var width = 8;

            yield return new Coordinate(x, y);

            while (count < number)
            {
                yield return new Coordinate(++x, y); // right

                var len = width / 4;

                for (var i = 0; i < len - 1; i++)
                    yield return new Coordinate(x, ++y); // up

                for (var i = 0; i < len; i++)
                    yield return new Coordinate(--x, y); // left

                for (var i = 0; i < len; i++)
                    yield return new Coordinate(x, --y); // down

                for (var i = 0; i < len; i++)
                    yield return new Coordinate(++x, y); // right

                count += width;
                width += 8;
            }
        }

        internal static IEnumerable<Coordinate> GenerateClockwise(int number)
        {
            var count = 1;
            var x = 1;
            var y = 1;
            var width = 8;

            yield return new Coordinate(x, y);

            while (count < number)
            {
                yield return new Coordinate(--x, y); // right

                var len = width / 4;

                for (var i = 0; i < len - 1; i++)
                    yield return new Coordinate(x, --y); // down

                for (var i = 0; i < len; i++)
                    yield return new Coordinate(--x, y); // left

                for (var i = 0; i < len; i++)
                    yield return new Coordinate(x, ++y); // up

                for (var i = 0; i < len; i++)
                    yield return new Coordinate(++x, y); // right

                count += width;
                width += 8;
            }
        }
    }
}
