using System.Collections;
using System.Collections.Generic;

namespace Pifagor.ClusterTree
{
    public struct Range: IEnumerable<int>
    {
        public int Skip;
        public int Take;

        public IEnumerator<int> GetEnumerator()
        {
            for (var i = Skip; i < Skip + Take; i++)
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