using System;
using System.Collections.Generic;

namespace Pifagor.ClusterTree
{
    public static class ClusterMath
    {
        /// <summary>
        /// Позволяет получить глубину, на которой находится кластер, при заданном порядке дерева
        /// </summary>
        /// <param name="treeBase">Порядок дерева</param>
        /// <param name="index">Номер кластера</param>
        /// <returns>Номер уровня, на котором находится нода</returns>
        internal static int GetLayerNumber(int treeBase, int index)
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
            return n - 1;
        }

        internal static int GetFirstIndexOfLayer(int treeBase, int layer)
        {
            if (layer < 0)
                throw new ArgumentOutOfRangeException(nameof(layer), layer, "Index must be greater or equal zero");
            if (treeBase < 1)
                throw new ArgumentOutOfRangeException(nameof(treeBase), treeBase, "Index must be greater than 0");
            if (layer == 0)
                return 0;
            var n = 0;
            var sum = 0;
            while (++n != layer)
            {
                sum += (int)Math.Pow(treeBase, n);
            }
            return sum + 1;
        }

        public static int[] GetPathToIndex(int treeBase, int index)
        {
            var layerNumber = GetLayerNumber(treeBase, index);
            var firstIndexOfLayer = GetFirstIndexOfLayer(treeBase, layerNumber);
            var x = index - firstIndexOfLayer;
            var path = new List<int>();
            while (x != 0)
            {
                path.Add(x % treeBase);
                x = x / treeBase;
            }
            while (path.Count < layerNumber)
                path.Insert(0,0);
            return path.ToArray();
        }
    }
}
