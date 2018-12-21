using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public struct OpcodeSample
    {
        public int[] Before { get; set; }
        public int[] Instruction { get; set; }
        public int[] After { get; set; }
    }

    public class Day16A : IProblem
    {
		protected readonly ParserType Parser;

        public Day16A(ParserType parser) { Parser = parser; }

        public Day16A() : this(new ParserType("day16.in")) { }

        public virtual string Solve()
        {
            var samples = GetOpcodeSamples();

            var opcodes = new List<OpcodeD16>
            {
                new AddiOpcode(), new AddrOpcode(),
                new MulrOpcode(), new MuliOpcode(),
                new BaniOpcode(), new BanrOpcode(),
                new BoriOpcode(), new BorrOpcode(),
                new SetiOpcode(), new SetrOpcode(),
                new GtirOpcode(), new GtriOpcode(), new GtrrOpcode(),
                new EqirOpcode(), new EqriOpcode(), new EqrrOpcode()
            };

            var threeOrMoreCount = 0;
            foreach (var sample in samples)
            {
                var count = 0;
                foreach (var opcode in opcodes)
                {
                    if (opcode.CanPerformTransform(sample))
                    {
                        count++;
                        if (count > 2)
                        {
                            threeOrMoreCount++;
                            break;
                        }
                    }
                }
            }

            return threeOrMoreCount.ToString();
        }

        protected IEnumerable<OpcodeSample> GetOpcodeSamples()
        {
            var samples = new List<OpcodeSample>();
            var lines = Parser.GetData().ToArray();
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Before:"))
                {
                    var beforeLine = lines[i++];
                    var before = beforeLine
                        .Substring(beforeLine.IndexOf('[') + 1, 10)
                        .Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                    var instruction = lines[i++].Split().Select(int.Parse).ToArray();

                    var afterLine = lines[i++];
                    var after = afterLine
                        .Substring(afterLine.IndexOf('[') + 1, 10)
                        .Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                    samples.Add(new OpcodeSample
                    {
                        Before = before,
                        Instruction = instruction,
                        After = after
                    });
                }
            }

            return samples;
        }
    }

    public class ComputerD16
    {
        public int[] Registers { get; }

        public ComputerD16() : this(new []{ 0, 0, 0, 0 }) { }

        public ComputerD16(int[] initialState)
        {
            Registers = new int[4];
            Array.Copy(initialState, Registers, initialState.Length);
        }
    }

    public abstract class OpcodeD16
    {
        public abstract string Name { get; }

        public abstract void Execute(ComputerD16 computer, int a, int b, int c);

        public bool CanPerformTransform(OpcodeSample sample)
        {
            var computer = new ComputerD16(sample.Before);

            Execute(computer, sample.Instruction[1], sample.Instruction[2], sample.Instruction[3]);

            return computer.Registers[0] == sample.After[0] &&
                   computer.Registers[1] == sample.After[1] &&
                   computer.Registers[2] == sample.After[2] &&
                   computer.Registers[3] == sample.After[3];
        }
    }

    public class AddrOpcode : OpcodeD16
    {
        public override string Name => "addr";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] + computer.Registers[b];
        }
    }
    public class AddiOpcode : OpcodeD16
    {
        public override string Name => "addi";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] + b;
        }
    }

    public class MulrOpcode : OpcodeD16
    {
        public override string Name => "mulr";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] * computer.Registers[b];
        }
    }

    public class MuliOpcode : OpcodeD16
    {
        public override string Name => "muli";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] * b;
        }
    }

    public class BanrOpcode : OpcodeD16
    {
        public override string Name => "banr";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] & computer.Registers[b];
        }
    }

    public class BaniOpcode : OpcodeD16
    {
        public override string Name => "bani";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] & b;
        }
    }

    public class BorrOpcode : OpcodeD16
    {
        public override string Name => "borr";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] | computer.Registers[b];
        }
    }

    public class BoriOpcode : OpcodeD16
    {
        public override string Name => "bori";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] | b;
        }
    }

    public class SetrOpcode : OpcodeD16
    {
        public override string Name => "setr";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a];
        }
    }

    public class SetiOpcode : OpcodeD16
    {
        public override string Name => "seti";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = a;
        }
    }

    public class GtirOpcode : OpcodeD16
    {
        public override string Name => "gtir";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = a > computer.Registers[b] ? 1 : 0;
        }
    }

    public class GtriOpcode : OpcodeD16
    {
        public override string Name => "gtri";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] > b ? 1 : 0;
        }
    }

    public class GtrrOpcode : OpcodeD16
    {
        public override string Name => "gtrr";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] > computer.Registers[b] ? 1 : 0;
        }
    }

    public class EqirOpcode : OpcodeD16
    {
        public override string Name => "eqir";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = a == computer.Registers[b] ? 1 : 0;
        }
    }

    public class EqriOpcode : OpcodeD16
    {
        public override string Name => "eqri";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = b == computer.Registers[a] ? 1 : 0;
        }
    }

    public class EqrrOpcode : OpcodeD16
    {
        public override string Name => "eqrr";

        public override void Execute(ComputerD16 computer, int a, int b, int c)
        {
            computer.Registers[c] = computer.Registers[a] == computer.Registers[b] ? 1 : 0;
        }
    }
}
