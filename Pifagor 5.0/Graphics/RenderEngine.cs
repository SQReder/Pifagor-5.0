using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pifagor.ClusterTree;
using Pifagor.Geometry;

namespace Pifagor.Graphics
{
    public class RenderEngine
    {
        private Size _ariaSize;
        private const int TakeBy = 1000;
        private readonly object _lock = new object();
        private Bitmap _lastBitmap;

        public Bitmap LastRendered 
        {
            get { return _lastBitmap; }
            private set
            {
                lock (_lock)
                {
                    _lastBitmap = value;
                }
            }
        }

        public RenderEngine(Size ariaSize)
        {
            _ariaSize = ariaSize;
            _lastBitmap = new Bitmap(1, 1);
        }

        public Task Render(CancellationToken token, List<FractalCluster> clusters)
        {
            return Task.Run(() => RenderMethod(token, clusters), token);
        }

        private void RenderMethod(CancellationToken token, List<FractalCluster> clusters)
        {

            var bitmap = new Bitmap(_ariaSize.Width, _ariaSize.Height);
            var g = System.Drawing.Graphics.FromImage(bitmap);

            var drawLock = new object();

            var ranges = ClusterMath.MakeRanges(clusters.Count, TakeBy);
            try
            {
                ranges.AsParallel()
                    .AsOrdered()
                    .WithCancellation(token)
                    .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                    .Select(r => DrawPartial(clusters.GetRange(r.Begin, r.Count)))
                    .ForAll(b =>
                    {
                        lock (drawLock)
                        {
                            g.DrawImageUnscaled(b, 0, 0);
                        }
                    });

                _lastBitmap = bitmap;
            }
            catch (OperationCanceledException e)
            {
                _lastBitmap = new Bitmap(1,1);
                throw;
            }
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
