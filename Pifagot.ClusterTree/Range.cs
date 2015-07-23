using System.Collections;
using System.Collections.Generic;

namespace Pifagor.ClusterTree
{
    public class Range: IEnumerable<int>
    {
        public readonly int Begin;
        public readonly int Count;

        public Range(int begin, int count)
        {
            Begin = begin;
            Count = count;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (var i = Begin; i < Begin + Count; i++)
            {
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}