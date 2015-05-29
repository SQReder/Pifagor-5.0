using System.Collections;
using System.Collections.Generic;

namespace Pifagor.Geometry
{
    class FractalCluster : IReadOnlyList<Segment>
    {
        private readonly List<Segment> _segments = new List<Segment>();

        private void Add(Segment v)
        {
            _segments.Add(v);
        }

        public Segment this[int i] => _segments[i];
        public int Count => _segments.Count;


        public IEnumerator<Segment> GetEnumerator()
        {
            return _segments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static FractalCluster operator *(FractalCluster fc, TransformationMatrix tm)
        {
            var result = new FractalCluster();
            foreach (var segment in fc._segments)
            {
                result.Add(segment * tm);
            }
            return result;
        }
    }
}
