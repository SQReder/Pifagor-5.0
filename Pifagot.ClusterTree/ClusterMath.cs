using System;
using System.Collections.Generic;
using System.Linq;
using Pifagor.Geometry;

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

       /// <summary>
       /// Позволяет получить индекс первого элемента на указанном уровне дерева
       /// </summary>
       /// <param name="treeBase">порядок дерева</param>
       /// <param name="layer">Номер уровня</param>
       /// <returns>Индекс первого элемента на уровне дерева</returns>
        public static int GetFirstIndexOfLayer(int treeBase, int layer)
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

        /// <summary>
        /// Переводит число из десятичной системы счисления в указанную
        /// </summary>
        /// <param name="base">Основание системы счисления</param>
        /// <param name="x">Число для перевода</param>
        /// <returns>Список значений позиций числа в указанной системе счисления</returns>
        internal static List<int> ConvertNumberToBase(int @base, int x)
        {
            var path = new List<int>();
            do
            {
                path.Add(x % @base);
                x = x / @base;
            }
            while (x != 0);

            return path;
        }


        /// <summary>
        /// Ищет путь до ноды с укзанным индексом в дереве заданного порядка
        /// </summary>
        /// <param name="treeBase">Порядок дерева</param>
        /// <param name="index">Индекс ноды в дереве</param>
        /// <returns>Список индексов нод внутри уровней дерева, ведущих к указанной ноде</returns>
        public static int[] GetPathToIndex(int treeBase, int index)
        {
            if (index == 0)
                return new int[0];

            var layerNumber = GetLayerNumber(treeBase, index);
            var firstIndexOfLayer = GetFirstIndexOfLayer(treeBase, layerNumber);
            var x = index - firstIndexOfLayer;
            var path = ConvertNumberToBase(treeBase, x);

            while (path.Count < layerNumber)
                path.Add(0);

            path.Reverse();
            return path.ToArray();
        }

        public static bool IsConvergent(FractalCluster cluster)
        {
            return !cluster.Segments.Any(segment => segment.Length >= 1);
        }

        public static double GetMaxLinearSize(FractalCluster cluster)
        {
            var segments = new List<Segment>();
            segments.AddRange(cluster.Segments);
            segments.AddRange(cluster.Decore);

            var max = 0.0;
            foreach (var first in segments)
            {
                var begin = first.Begin.Length;
                var end = first.End.Length;
                max = Math.Max(max, Math.Max(begin, end));
            }
            return max;
        }

        /// <summary>
        /// Делит общее количество элементов на промежутки указанного размера
        /// </summary>
        /// <param name="count">Общее количество элементов</param>
        /// <param name="takeBy">Максимальный размер промежутка</param>
        /// <returns>Набор промежутков заданного размера</returns>
        public static List<Range> MakeRanges(int count, int takeBy)
        {
            var ranges = new List<Range>();
            for (var skip = 0; skip < count; skip += takeBy)
            {
                var take = takeBy;
                if (skip + take > count)
                    take = count - skip;

                ranges.Add(new Range(skip, take));
            }
            return ranges;
        }
    }
}
