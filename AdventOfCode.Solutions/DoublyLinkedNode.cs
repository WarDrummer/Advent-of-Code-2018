using System.Collections.Generic;

namespace AdventOfCode.Solutions
{
    public class DoublyLinkedNode<TValue>
    {
        public TValue Value { get; set; }
        public DoublyLinkedNode<TValue> Next { get; set; }
        public DoublyLinkedNode<TValue> Previous { get; set; }

        public DoublyLinkedNode(TValue value, DoublyLinkedNode<TValue> next = null, DoublyLinkedNode<TValue> previous = null)
        {
            Value = value;
            Next = next;
            Previous = previous;
        }

        public void Remove()
        {
            if (Next != null && Previous != null)
                Previous.Next = Next;
        }

        public DoublyLinkedNode<TValue> Insert(TValue value)
        {
            var newNode = new DoublyLinkedNode<TValue>(value);
            var oldNext = Next;

            Next = newNode;
            newNode.Previous = this;

            if (oldNext != null)
            {
                oldNext.Previous = newNode;
                newNode.Next = oldNext;
            }
            else
            {
                Previous = newNode;
                newNode.Next = this;
            }

            return newNode;
        }

        public DoublyLinkedNode<TValue> GetPrevious(int numberBack)
        {
            var node = this;
            for (var i = 0; i < numberBack; i++)
            {
                node = node.Previous;
            }

            return node;
        }

        public override string ToString()
        {
            var items = new List<TValue>();
            var current = this;
            do
            {
                items.Add(current.Value);
                current = current.Next;
            } while (current != this && current != null);
            return string.Join(", ", items);
        }
    }
}