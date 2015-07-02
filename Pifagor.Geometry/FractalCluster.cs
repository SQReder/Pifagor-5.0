using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Pifagor.Geometry
{
    public class FractalCluster: IDrawable
    {
        private readonly List<Segment> _segments = new List<Segment>();
        private readonly List<Segment> _decoratingSegments = new List<Segment>(); 

        public void Add(Segment v)
        {
            _segments.Add(v);
        }

        private void AddDecore(Segment segment)
        {
            _decoratingSegments.Add(segment);
        }

        public List<Segment> Segments => _segments;
        public List<Segment> Decore => _decoratingSegments;

        public FractalCluster TransformWith(Segment seg)
        {
            var result = new FractalCluster();
            foreach (var segment in _segments)
            {
                result.Add(segment.TransformWith(seg));
            }
            foreach (var decoratingSegment in _decoratingSegments)
            {
                result.AddDecore(decoratingSegment.TransformWith(seg));
            }
            return result;
        }

        public void Draw(Graphics g, Pen pen)
        {
            foreach (var segment in _segments)
            {
                segment.Draw(g, pen);
            }

            foreach (var segment in _decoratingSegments)
            {
                segment.Draw(g, pen);
            }
        }
    }
}
