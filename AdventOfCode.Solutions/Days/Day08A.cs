using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Solutions.Parsers;
using AdventOfCode.Solutions.Problem;

namespace AdventOfCode.Solutions.Days
{
    using ParserType = SingleLineStringParser;

    public class Day8A : IProblem
    {
        protected readonly ParserType Parser;

        public Day8A(ParserType parser) { Parser = parser; }

        public Day8A() : this(new ParserType("day08.in")) { }

        public virtual string Solve()
        {
            var input = Parser.GetData().Split(' ').Select(int.Parse).ToList();
            var index = 0;
            ReadData(input, ref index);
            return MemoryNode.MetadataTotal.ToString();
        }

        public MemoryNode ReadData(List<int> data, ref int index, MemoryNode node = null)
        {
            var childCount = data[index++];
            var metadataCount = data[index++];
            var memoryNode = new MemoryNode(childCount, metadataCount);

            for (var i = 0; i < childCount; i++)
                memoryNode.AddChild(ReadData(data, ref index, memoryNode));

            var end = index + metadataCount;
            for (var i = index; i < end; i++)
                memoryNode.AddMetadata(data[index++]);

            return memoryNode;
        }
    }

    public class MemoryNode
    {
        private static char _nextName = 'A';
        public static int MetadataTotal;

        private readonly char _name;
        private readonly List<int> _metadata = new List<int>();
        private readonly List<MemoryNode> _children = new List<MemoryNode>();

        public int ChildCount { get; }
        public int MetadataCount { get; }

        public int Value
        {
            get
            {
                var count = 0;
                if (ChildCount == 0)
                {
                    foreach (var m in _metadata)
                        count += m;
                }
                else
                {
                    var childCount = _children.Count;
                    foreach (var m in _metadata)
                        if (m <= childCount)
                            count += _children[m-1].Value;
                }
                return count;
            }
        }

        public MemoryNode(int childCount, int metadataCount)
        {
            _name = _nextName++;
            ChildCount = childCount;
            MetadataCount = metadataCount;
        }

        public void AddMetadata(int metadata)
        {
            MetadataTotal += metadata;
            _metadata.Add(metadata);
        }

        public void AddChild(MemoryNode child)
        {
            _children.Add(child);
        }

        public override string ToString()
        {
            var childNames = string.Join(", ", _children.Select(c => c._name).ToList());
            var metadata = string.Join(", ", _metadata);
            return $"{_name} which has {ChildCount} child nodes ({childNames}) and {MetadataCount} metadata entries ({metadata})";
        }
    }
}
