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

        public static int CreateHash(this char[][] chars)
        {
            var hash = chars.Length;
            for (var y = 0; y < chars.Length; y++)
            {
                hash = unchecked(hash * 314159 + new string(chars[y]).GetHashCode());
                //for (var x = 0; x < chars[0].Length; x++)
                //{
                //    hash = unchecked(hash * 314159 + chars[y][x]);
                //}
            }
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
