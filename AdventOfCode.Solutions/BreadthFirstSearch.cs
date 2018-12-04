using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions
{
    public interface IBfsNode<THashType> : IComparable<IBfsNode<THashType>>
    {
        IEnumerable<IBfsNode<THashType>> GetNextNodes();
        THashType UniqueIdentifier { get; }
    }

    public class BreadthFirstSearch<THashType>
    {
        private readonly IBfsNode<THashType> _goal;

        public BreadthFirstSearch(IBfsNode<THashType> goal)
        {
            _goal = goal;
        }

        public int GetMinimumNumberOfMoves(IBfsNode<THashType> start)
        {
            var matchFound = false;
            var numberOfMoves = 0;

            var currentNodes = new Queue<IBfsNode<THashType>>();
            currentNodes.Enqueue(start);

            var nextNodes = new Queue<IBfsNode<THashType>>();
            var visited = new HashSet<THashType>();

            while (currentNodes.Count > 0)
            {
                var currentNode = currentNodes.Dequeue();

                if (currentNode.CompareTo(_goal)== 0)
                {
                    matchFound = true;
                    break;
                }

                foreach (var nextNode in currentNode.GetNextNodes())
                {
                    if (!visited.Contains(nextNode.UniqueIdentifier))
                    {
                        nextNodes.Enqueue(nextNode);
                        visited.Add(nextNode.UniqueIdentifier);
                    }
                }

                if (currentNodes.Count == 0)
                {
                    currentNodes = nextNodes;
                    nextNodes = new Queue<IBfsNode<THashType>>();
                    numberOfMoves += 1;
                }
            }

            return matchFound ? numberOfMoves : -1;
        }
    }
}
