using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pifagor.Geometry;

namespace Pifagor.ClusterTree
{
    public class CachedFractal
    {
        private readonly IClusterCache _cache = new DictionaryClusterCache();
        private readonly FractalCluster _fractal;
        private readonly int _treeBase;

        public CachedFractal(FractalCluster fractal)
        {
            _fractal = fractal;
            _treeBase = _fractal.Segments.Count;
        }

        public Task<List<FractalCluster>> ProcessLevels(CancellationToken token, int d)
        {
            return Task.Run(() =>
            {
                var lastIndex = ClusterMath.GetFirstIndexOfLayer(_treeBase, d + 1);
                var ranges = ClusterMath.MakeRanges(lastIndex, 100);
                return ranges.AsParallel().AsOrdered().WithCancellation(token)
                    .SelectMany(r => ListClustersInRange(r, _treeBase)).ToList();
            }, token);
        }

        private IEnumerable<FractalCluster> ListClustersInRange(Range range, int treeBase)
        {
            return range.Select(i => ClusterMath.GetPathToIndex(treeBase, i)).Select(Transform);
        }

        private FractalCluster Transform(int[] path)
        {
            var cached = _cache.Find(path);
            if (cached != null)
                return cached;

            var tuple = GetPartialTransformed(_fractal, path);
            var transformed = tuple.Item1;
            var transforms = tuple.Item2;
            transformed = transforms.Select(i => _fractal.Segments[i])
                .Aggregate(transformed, (current, segment) => current.TransformWith(segment));
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