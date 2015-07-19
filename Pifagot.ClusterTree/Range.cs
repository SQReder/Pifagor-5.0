using System.Collections;
using System.Collections.Generic;

namespace Pifagor.ClusterTree
{
    public struct Range: IEnumerable<int>
    {
        public int Begin;
        public int Count;

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