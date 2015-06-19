using System.Collections.Generic;
using System.Linq;
using Pifagor.Geometry;

namespace Pifagor.ClusterTree
{
    interface IClusterCache
    {
        void Add(IEnumerable<int> path, FractalCluster cluster);
        FractalCluster Find(IEnumerable<int> path);
        void Clear();
        bool Has(IEnumerable<int> path);
    }

    class DictionaryClusterCache : IClusterCache
    {
        private Dictionary<string, FractalCluster> _dictionary = new Dictionary<string, FractalCluster>();

        private static string MakeKey(IEnumerable<int> path)
        {
            return string.Join("", path.Select(x => x.ToString()).ToArray());
        }

        public void Add(IEnumerable<int> path, FractalCluster cluster)
        {
            var key = MakeKey(path);
            _dictionary.Add(key, cluster);
        }

        public FractalCluster Find(IEnumerable<int> path)
        {
            FractalCluster value;
            var result = _dictionary.TryGetValue(MakeKey(path), out value);
            return result ? value : null;
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Has(IEnumerable<int> path)
        {
            var key = MakeKey(path);
            return _dictionary.ContainsKey(key);
        }
    }
}
