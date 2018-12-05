using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Extensions
{
    public static class LinkedListExtensions
    {
        public static Node<TValue> BuildCircularList<TValue>(this TValue[] sequence)
        {
            if (sequence.Length < 1)
                return null;

            var head = new Node<TValue> { Value = sequence[0] };
            var current = head;
            for (var index = 1; index < sequence.Length; index++)
            {
                var val = sequence[index];
                current.Next = new Node<TValue> {Value = val};
                current = current.Next;
            }
            current.Next = head;
            return head;
        }
        public static Node<TValue> BuildLinkedList<TValue>(this TValue[] sequence)
        {
            if (sequence.Length < 1)
                return null;

            var head = new Node<TValue> { Value = sequence[0] };
            var current = head;
            for (var index = 1; index < sequence.Length; index++)
            {
                var val = sequence[index];
                current.Next = new Node<TValue> { Value = val };
                current = current.Next;
            }
            return head;
        }

        public static Node<char> BuildLinkedList(this string sequence)
        {
            if (sequence.Length < 1)
                return null;

            var head = new Node<char> { Value = sequence[0] };
            var current = head;
            for (var index = 1; index < sequence.Length; index++)
            {
                var val = sequence[index];
                current.Next = new Node<char> { Value = val };
                current = current.Next;
            }
            return head;
        }

        public static List<TValue> ToList<TValue>(this Node<TValue> head)
        {
            var current = head;
            var list = new List<TValue>();
            do
            {
                list.Add(current.Value);
                current = current.Next;
            } while (current != head && current != null);
            return list;
        }

        public static void ToConsole<TValue>(this Node<TValue> head)
        {
            Console.WriteLine(string.Join(", ", head.ToList()));
        }
    }
}