using System.Text;

namespace AdventOfCode.Solutions
{
    public class Node<TValue>
    {
        public TValue Value { get; set; }
        public Node<TValue> Next { get; set; }

        public Node() { }

        public Node(TValue value, Node<TValue> next = null)
        {
            Value = value;
            Next = next;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var current = this;
            do
            {
                sb.Append(current.Value);
                current = current.Next;
            } while (current != this && current != null);
            return sb.ToString();
        }
    }
}