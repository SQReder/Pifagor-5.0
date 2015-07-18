using System;
using System.Collections.Generic;
using System.Linq;
using Pifagor.Geometry;

namespace Pifagor.ClusterTree
{
    interface IClusterCache
    {
        void Add(int[] path, FractalCluster cluster);
        FractalCluster Find(int[] path);
        void Clear();
        bool Has(int[] path);
    }

    class DictionaryClusterCache : IClusterCache
    {
        private readonly Dictionary<string, FractalCluster> _dictionary = new Dictionary<string, FractalCluster>();

        private static string MakeKey(int[] path)
        {
            return string.Join("", path);
        }

        public void Add(int[] path, FractalCluster cluster)
        {
            var key = MakeKey(path);
            _dictionary[key] = cluster;
        }

        public FractalCluster Find(int[] path)
        {
            FractalCluster value;
            var result = _dictionary.TryGetValue(MakeKey(path), out value);
            return result ? value : null;
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Has(int[] path)
        {
            var key = MakeKey(path);
            return _dictionary.ContainsKey(key);
        }
    }
}
