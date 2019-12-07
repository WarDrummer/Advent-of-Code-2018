using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    public class Day11A : IProblem
    {
        public virtual string Solve()
        {
            var size = 300;
            var grid = CreateFuelCellGrid(size);
            return GetCoordinatesWithMaxTotalPower(size, grid);
        }

        private static string GetCoordinatesWithMaxTotalPower(int size, FuelCell[,] grid)
        {
            var max = long.MinValue;
            var maxCoords = "";
            for (var x = 0; x < size - 2; x++)
            {
                for (var y = 0; y < size - 2; y++)
                {
                    long totalPower = 0;

                    for (var level = 0; level < 3; level++)
                    {
                        var totalPowerForLevel = GetTotalPowerForLevel(grid, level, x, y);
                        totalPower += totalPowerForLevel;
                    }

                    if (totalPower > max)
                    {
                        max = totalPower;
                        maxCoords = $"{x},{y}";
                    }
                }
            }

            return maxCoords;
        }

        protected static long GetTotalPowerForLevel(FuelCell[,] grid, int level, int x, int y)
        {
            long totalPowerForLevel = 0;
            for (var j = 0; j < level; j++)
                totalPowerForLevel += grid[x + level, y + j].PowerLevel;

            for (var j = 0; j < level; j++)
                totalPowerForLevel += grid[x + j, y + level].PowerLevel;

            totalPowerForLevel += grid[x + level, y + level].PowerLevel;
            return totalPowerForLevel;
        }

        protected static FuelCell[,] CreateFuelCellGrid(int size)
        {
            var grid = new FuelCell[size, size];
            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
                grid[x, y] = new FuelCell(x, y);
            return grid;
        }
    }

    public class FuelCell
    {
        public static long GridSerialNumber = 1718;

        public long X { get; }
        public long Y { get; }
        public long RackId { get; }
        public long PowerLevel { get;  }

        public FuelCell(int x, int y)
        {
            X = x;
            Y = y;
            RackId = x + 10;
            PowerLevel = (RackId * y + GridSerialNumber) * RackId;
            PowerLevel = (PowerLevel / 100) % 10; // get hundredth place
            PowerLevel -= 5;
        }


    }
}
