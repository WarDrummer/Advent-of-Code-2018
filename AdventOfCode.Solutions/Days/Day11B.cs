using System;

namespace AdventOfCode.Solutions.Days
{
    public class Day11B : Day11A
    {
        public override string Solve()
        {
            var size = 300;
            var grid = CreateFuelCellGrid(size);
            return GetCoordinatesWithMaxTotalPowerMultiLevel(size, grid);
        }

        private static string GetCoordinatesWithMaxTotalPowerMultiLevel(int size, FuelCell[,] grid)
        {
            var max = long.MinValue;
            int maxX=0, maxY=0, maxEncounteredLevel=0;
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    long totalPower = 0;
                    var maxLevel = Math.Min(size - x, size - y);
                    for (var level = 0; level < maxLevel; level++)
                    {
                        var totalPowerForLevel = GetTotalPowerForLevel(grid, level, x, y);
                        totalPower += totalPowerForLevel;
                        if (totalPower > max)
                        {
                            max = totalPower;
                            maxX = x;
                            maxY = y;
                            maxEncounteredLevel = level + 1;
                        }
                    }
                }
            }

            return $"{maxX},{maxY},{maxEncounteredLevel}";
        }
    }
}
