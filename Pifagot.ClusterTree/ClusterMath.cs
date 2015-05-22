using System;

namespace Pifagor.ClusterTree
{
    public static class ClusterMath
    {
        /// <summary>
        /// Позволяет получить глубину, на которой находится кластер, при заданном порядке дерева
        /// </summary>
        /// <param name="index">Номер кластера</param>
        /// <param name="treeBase">Порядок дерева</param>
        /// <returns>Номер уровня, на котором находится нода</returns>
        public static int GetLayerNumber(int index, int treeBase)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be greater or equal zero");
            if (treeBase < 1)
                throw new ArgumentOutOfRangeException(nameof(treeBase), treeBase, "Index must be greater than 0");
            if (index == 0)
                return 0;

            var n = 1;
            do
            {
                index -= (int)Math.Floor(Math.Pow(treeBase, n));
                n++;
            }
            while (index > 0);
            return n-1;
        }
    }
}
