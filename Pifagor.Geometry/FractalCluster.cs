using System.Collections;
using System.Collections.Generic;

namespace Pifagor.Geometry
{
    public struct FractalCluster: IReadOnlyList<Segment>
    {
        private List<Segment> _segments;

        public void Add(Segment v)
        {
            if (_segments == null)
                _segments = new List<Segment>();
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

        public FractalCluster TransformWith(Segment seg)
        {
            var result = new FractalCluster();
            foreach (var segment in _segments)
            {
                result.Add(segment.TransformWith(seg));
            }
            return result;
        }
    }
}
