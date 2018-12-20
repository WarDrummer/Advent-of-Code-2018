namespace AdventOfCode.Solutions.Days
{
    public class Day17B : Day17A
    {
        public override string Solve()
        {
            var results = ParseInput();
            var clay = results.Item1;
            var boundingBox = results.Item2;

            var tracer = new WaterTracer(clay, boundingBox);
            tracer.Compute();
            tracer.CountWaterTiles();
            return tracer.PooledWaterCount.ToString();
        }
    }
}
