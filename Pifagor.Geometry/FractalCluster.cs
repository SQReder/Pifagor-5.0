using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

        public Rectangle ClipRectangle()
        {
            var segments = new List<Segment>();
            segments.AddRange(Segments);
            segments.AddRange(Decore);

            var xs = segments.SelectMany(s => new[] { s.Begin.X, s.End.X }).ToList();
            var ys = segments.SelectMany(s => new[] { s.Begin.Y, s.End.Y }).ToList();

            var left = Math.Floor(xs.Min());
            var top = Math.Floor(ys.Min());
            // 1 прибавляется что-бы расширить границы квадрата вправо
            var right = Math.Floor(xs.Max()) + 1;
            var bottom = Math.Floor(ys.Max()) + 1;
            var x = (int)left;
            var y = (int)top;
            var width = (int)(right - left);
            var height = (int)(bottom - top);

            return new Rectangle(x, y, width, height);
        }
    }
}
