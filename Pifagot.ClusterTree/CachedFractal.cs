using System;
using System.Collections.Generic;
using System.Linq;
using Pifagor.Geometry;

namespace Pifagor.ClusterTree
{
    public class CachedFractal
    {
        private readonly IClusterCache _cache = new DictionaryClusterCache();
        private readonly FractalCluster _fractal;

        public CachedFractal(FractalCluster fractal)
        {
            _fractal = fractal;
        }

        public IEnumerable<FractalCluster> ProcessLevels(int d)
        {
            var treeBase = _fractal.Count;
            var lastIndex = ClusterMath.GetFirstIndexOfLayer(treeBase, d + 1);
            var result = new List<FractalCluster>();
            for (var i = 0; i != lastIndex; ++i)
            {
                var path = ClusterMath.GetPathToIndex(treeBase, i);
                var cluster = Transform(path);
                result.Add(cluster);
            }
            return result;
        }

        private FractalCluster Transform(int[] path)
        {
            var cached = _cache.Find(path);
            if (cached != null)
                return cached;

            var tuple = GetPartialTransformed(_fractal, path);
            var transformed = tuple.Item1;
            var transforms = tuple.Item2;
            for (var index = 0; index < transforms.Length; index++)
            {
                var i = transforms[index];
                var segment = _fractal[i];
                transformed = transformed.TransformWith(segment);
            }
            _cache.Add(path, transformed);
            return transformed;
        }

        private Tuple<FractalCluster, int[]> GetPartialTransformed(FractalCluster cluster, int[] path)
        {
            for (var length = path.Length; length != 0; --length)
            {
                var shortPath = path.Take(length).ToArray();
                var cached = _cache.Find(shortPath);
                if (cached != null)
                {
                    var tail = path.TakeLast(path.Length - length).ToArray();
                    return new Tuple<FractalCluster, int[]>(cached, tail);
                }
            }

            return new Tuple<FractalCluster, int[]>(cluster, path);
        }
    }

    public static class Extensions
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> collection, int n)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "n must be 0 or greater");

            var temp = new LinkedList<T>();

            foreach (var value in collection)
            {
                temp.AddLast(value);
                if (temp.Count > n)
                    temp.RemoveFirst();
            }

            return temp;
        }
    }
}