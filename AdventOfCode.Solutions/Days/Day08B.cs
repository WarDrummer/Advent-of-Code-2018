using System.Linq;

namespace AdventOfCode.Solutions.Days
{
    public class Day8B : Day8A
    {
        public override string Solve()
        {
            var input = Parser.GetData().Split(' ').Select(int.Parse).ToList();
            var index = 0;
            var head = ReadData(input, ref index);
            return head.Value.ToString();
        }
    }
}
