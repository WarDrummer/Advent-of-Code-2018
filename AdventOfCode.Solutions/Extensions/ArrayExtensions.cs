namespace AdventOfCode.Solutions.Extensions
{
    public static class ArrayExtensions
    {
        public static int CreateHash(this int[] ints)
        {
            var hash = ints.Length;
            foreach (var i in ints)
                hash = unchecked(hash * 314159 + i);
            return hash;
        }

        public static int CreateHash(this string s)
        {
            var hash = s.Length;
            foreach (var i in s)
                hash = unchecked(hash * 314159 + i);
            return hash;
        }
    }
}
