using System;

namespace AdventOfCode.Solutions.Extensions
{
    public static class MatrixExtensions
    {
        public static char[][] Rotate(this char[][] matrix)
        {
            var n = matrix.Length;
            for (var layer = 0; layer < n / 2; layer++)
            {
                var first = layer;
                var last = n - 1 - layer;
                for (var i = first; i < last; i++)
                {
                    var offset = i - first;
                    var top = matrix[first][i];
                    matrix[first][i] = matrix[last - offset][first];
                    matrix[last - offset][first] = matrix[last][last - offset];
                    matrix[last][last - offset] = matrix[i][last];
                    matrix[i][last] = top;
                }
            }

            return matrix;
        }

        public static char[][] Flip(this char[][] matrix)
        {
            foreach (var row in matrix)
                Array.Reverse(row);
            return matrix;
        }
    }
}