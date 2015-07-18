using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Pifagor.Geometry;

namespace Pifagor.Graphics
{
    public class RenderEngine
    {
        private struct Range
        {
            public int Skip;
            public int Take;
        }

        private Bitmap _bitmap;
        public bool RenderInProcess { get; private set; }
        private Size _ariaSize;
        private const int TakeBy = 1000;

        public RenderEngine(Size ariaSize)
        {
            _ariaSize = ariaSize;
        }

        public Bitmap Result
        {
            get
            {
                if (RenderInProcess)
                    throw new InvalidOperationException();
                return _bitmap;
            }
        }

        public void StartRender()
        {
            // todo lock
            if (RenderInProcess)
                throw new InvalidOperationException();
            RenderInProcess = true;
            _bitmap = new Bitmap(1000, 1000);

        }

        public void EndRender()
        {
            // todo lock
            if (!RenderInProcess)
                throw new InvalidOperationException();
            RenderInProcess = false;
        }

        public void Render(List<FractalCluster> clusters)
        {
            if (!RenderInProcess)
                throw new InvalidOperationException();
            RenderMethod(clusters);
        }

        private void RenderMethod(List<FractalCluster> clusters)
        {

            var g = System.Drawing.Graphics.FromImage(_bitmap);

            var ranges = MakeRanges(clusters.Count, TakeBy);
            ranges.AsParallel().AsOrdered()
                .Select(r => DrawPartial(clusters.GetRange(r.Skip, r.Take)))
                .ForAll(b => g.DrawImageUnscaled(b, 0, 0));
            
            //DrawFractal(g, clusters);
        }

        private List<Range> MakeRanges(int count, int takeBy)
        {
            var ranges = new List<Range>();
            for (var skip = 0; skip < count; skip += takeBy)
            {
                var take = takeBy;
                if (skip + take > count)
                    take = count - skip;

                ranges.Add(new Range {Skip = skip, Take = take});
            }
            return ranges;
        }

        private Bitmap DrawPartial(List<FractalCluster> clusters)
        {
            var bitmap = new Bitmap(_ariaSize.Width, _ariaSize.Height);
            var graphics = System.Drawing.Graphics.FromImage(bitmap);

            DrawFractal(graphics, clusters);

            return bitmap;
        }

        private void DrawFractal(System.Drawing.Graphics g, List<FractalCluster> clusters)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;

            // scale segment
            var seg = new Segment(new Vector(650, 0), new Vector(750, 0));
            foreach (var cluster in clusters)
            {
                var fc = cluster.TransformWith(seg);
                fc.Draw(g, Pens.Black);
            }
        }
    }
}
