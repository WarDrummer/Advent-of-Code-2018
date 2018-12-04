using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Solutions.Parsers
{
    public abstract class InputParser<T> : IInputParser<T>
    {
        private readonly string _inputFile;

        protected InputParser(string inputFile)
        {
            _inputFile = inputFile;
        }

        protected IEnumerable<string> GetInput()
        {
            using (var sr = new StreamReader($"Input\\{_inputFile}"))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }

        public abstract T GetData();
    }
}