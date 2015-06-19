using System;
using System.Collections.Generic;
using System.Linq;
using Pifagor.Geometry;

namespace Pifagor.ClusterTree
{
    public class Node: IDisposable
    {
        public Node(int key)
        {
            Key = key;
            Value = null;
            ChildNodes = new List<Node>();
        }

        public int Key { get; }
        public FractalCluster Value { get; set; }

        public readonly List<Node> ChildNodes;

        public void Dispose()
        {
            foreach (var node in ChildNodes)
            {
                node.Dispose();
            }
            ChildNodes.Clear();
        }
    }

    class Tree : IDisposable
    {
        private readonly Node _root = new Node(0);

        public void Insert(IEnumerable<int> path, FractalCluster value)
        {
            var enumerator = path.GetEnumerator();
            var current = _root;
            while (enumerator.MoveNext())
            {
                var key = enumerator.Current;
                var found = current.ChildNodes.FirstOrDefault(n => n.Key == key);
                if (found == null)
                {
                    found = new Node(key);
                    current.ChildNodes.Add(found);
                }
                current = found;
            }
            current.Value = value;
        }

        public Node Find(IEnumerable<int> path)
        {
            var enumerator = path.GetEnumerator();
            var current = _root;
            while (enumerator.MoveNext())
            {
                var key = enumerator.Current;
                var node = current.ChildNodes.FirstOrDefault(n => n.Key == key);
                if (node != null)
                {
                    current = node;
                }
                else
                {
                    return null;
                }
            }

            return current;
        }

        public void Dispose()
        {
            _root.Dispose();
        }
    }
}