using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Instruction
    {
        public int Time { get; set; }

        private readonly List<Instruction> _prerequisites = new List<Instruction>();
        public char Name { get; }

        public Instruction(char name)
        {
            Name = name;
            Time = Name - 'A' + 61;
        }

        public bool HasPrerequisites()
        {
            return _prerequisites.Count > 0;
        }

        public void AddPrerequisite(Instruction i)
        {
            if (!_prerequisites.Contains(i))
                _prerequisites.Add(i);
        }

        public void RemovePrerequisite(Instruction i)
        {
            if (_prerequisites.Contains(i))
                _prerequisites.Remove(i);
        }

        public override bool Equals(object obj)
        {
            if (obj is Instruction instruction)
                return Equals(instruction);
            return false;
        }

        protected bool Equals(Instruction other)
        {
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name;
        }
    }

    public class Day7A : IProblem
    {
        private readonly ParserType _parser;

        public Day7A(ParserType parser) { _parser = parser; }

        public Day7A() : this(new ParserType("day07.in")) { }

        public virtual string Solve()
        {
            var instructions = GetInstructions();
            return GetStepOrder(instructions);
        }

        protected List<Instruction> GetInstructions()
        {
            var instructions = new List<Instruction>();
            foreach (var instruction in _parser.GetData())
            {
                var prereq = instruction[5];
                var step = instruction[36];

                if (instructions.All(s => s.Name != step))
                    instructions.Add(new Instruction(step));

                if (instructions.All(s => s.Name != prereq))
                    instructions.Add(new Instruction(prereq));

                instructions.First(i => i.Name == step).AddPrerequisite(
                    instructions.First(i => i.Name == prereq));
            }

            return instructions;
        }

        private static string GetStepOrder(ICollection<Instruction> instructions)
        {
            var sb = new StringBuilder();

            while (instructions.Count > 0)
            {
                var steps = instructions.Where(i => !i.HasPrerequisites()).OrderBy(s => s.Name).ToList();
                var step = steps[0];
                sb.Append(step.Name);
                instructions.Remove(step);
                foreach (var instruction in instructions)
                    instruction.RemovePrerequisite(step);
            }

            var result = sb.ToString();
            return result;
        }
    }

}
