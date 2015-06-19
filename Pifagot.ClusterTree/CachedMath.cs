using System.Collections.Generic;
using Pifagor.Geometry;

namespace Pifagor.ClusterTree
{
    public static class CachedMath
    {
        private static readonly IClusterCache Cache = new DictionaryClusterCache();

        public static IEnumerable<FractalCluster> Populate(FractalCluster cluster, int d)
        {
            var treeBase = cluster.Count;
            var lastIndex = ClusterMath.GetFirstIndexOfLayer(treeBase, d + 1);
            for (var i = 0; i != lastIndex; ++i)
            {
                var path = ClusterMath.GetPathToIndex(treeBase, i);
                yield return Transform(cluster, path);
            }
        }

        private static FractalCluster Transform(FractalCluster cl, int[] path)
        {
            var cached = Cache.Find(path);
            if (cached != null)
                return cached;

            var c = cl;
            for (int i = 0; i != path.Length; ++i)
            {
                var idx = path[i];
                var segment = cl[idx];
                c = c.TransformWith(segment);
            }
            Cache.Add(path, c);
            return c;
        }
    }
}