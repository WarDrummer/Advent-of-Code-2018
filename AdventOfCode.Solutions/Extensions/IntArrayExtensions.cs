namespace AdventOfCode.Solutions.Extensions
{
    public static class IntArrayExtensions
    {
        public static void MinMax(this int[] numbers, out int min, out int max)
        {
            max = int.MinValue;
            min = int.MaxValue;

            foreach (var i in numbers)
            {
                if (i < min) min = i;
                if (i > max) max = i;
            }
        }
    }
}
