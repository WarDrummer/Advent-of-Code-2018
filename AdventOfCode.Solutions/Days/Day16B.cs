using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Parsers;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = MultiLineStringParser;

    public class Day16B : Day16A
    {
        protected readonly ParserType Parser;

        public Day16B(ParserType parser) { Parser = parser; }

        public Day16B() : this(new ParserType("day16.in")) { }
    
        public override string Solve()
        {
            var samples = GetOpcodeSamples();
            var opcodeLookup = GetOpcodeLookup(samples);
            var instructions = GetInstructionSet();

            var computer = new ComputerD16();
            foreach (var i in instructions)
            {
                var instruction = opcodeLookup[i[0]];
                instruction.Execute(computer, i[1], i[2], i[3]);
            }

            return computer.Registers[0].ToString();
        }

        private static Dictionary<int, OpcodeD16> GetOpcodeLookup(IEnumerable<OpcodeSample> samples)
        {
            var opcodes = new List<OpcodeD16>
            {
                new AddiOpcode(),
                new AddrOpcode(),
                new MulrOpcode(),
                new MuliOpcode(),
                new BaniOpcode(),
                new BanrOpcode(),
                new BoriOpcode(),
                new BorrOpcode(),
                new SetiOpcode(),
                new SetrOpcode(),
                new GtirOpcode(),
                new GtriOpcode(),
                new GtrrOpcode(),
                new EqirOpcode(),
                new EqriOpcode(),
                new EqrrOpcode()
            };

            var opcodeLookup = new Dictionary<int, OpcodeD16>();
            while (opcodeLookup.Keys.Count < 16)
            {
                foreach (var sample in samples)
                {
                    OpcodeD16 match = null;
                    foreach (var opcode in opcodes)
                    {
                        if (opcode.CanPerformTransform(sample))
                        {
                            if (match != null)
                            {
                                match = null;
                                break;
                            }

                            match = opcode;
                        }
                    }

                    if (match != null)
                    {
                        opcodeLookup[sample.Instruction[0]] = match;
                        opcodes.Remove(match);
                    }
                }
            }

            return opcodeLookup;
        }

        protected IEnumerable<int[]> GetInstructionSet()
        {
            var blankCount = 0;
            var startReading = false;
            var instructions = new List<int[]>();
            var lines = Parser.GetData().ToArray();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    blankCount++;
                    if (blankCount == 3)
                        startReading = true;
                }
                else if (startReading)
                    instructions.Add(line.Split(' ').Select(int.Parse).ToArray());
                else
                    blankCount = 0;
            }

            return instructions;
        }
    }
}
