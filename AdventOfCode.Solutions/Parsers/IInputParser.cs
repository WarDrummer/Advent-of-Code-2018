namespace AdventOfCode.Solutions.Parsers
{
    public interface IInputParser<T>
    {
        T GetData();
    }
}