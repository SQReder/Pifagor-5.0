using System.Collections.Generic;
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
            for (var i = 0; i != lastIndex; ++i)
            {
                var path = ClusterMath.GetPathToIndex(treeBase, i);
                yield return Transform(path);
            }
        }

        private FractalCluster Transform(IReadOnlyList<int> path)
        {
            var cached = _cache.Find(path);
            if (cached != null)
                return cached;

            var c = _fractal;
            for (int i = 0; i != path.Count; ++i)
            {
                var idx = path[i];
                var segment = _fractal[idx];
                c = c.TransformWith(segment);
            }
            _cache.Add(path, c);
            return c;
        }
    }
}