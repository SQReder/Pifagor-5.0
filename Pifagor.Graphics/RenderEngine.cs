using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Pifagor.Geometry;

namespace Pifagor.Graphics
{
    public class RenderEngine
    {
        private Bitmap _bitmap;
        public bool RenderInProcess { get; private set; }

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
            if (RenderInProcess)
                throw new InvalidOperationException();
            _bitmap = new Bitmap(1000, 1000);
        }

        public void EndRender()
        {
            if (!RenderInProcess)
                throw new InvalidOperationException();
        }

        public Bitmap Render(List<FractalCluster> clusters)
        {
            if (!RenderInProcess)
                throw new InvalidOperationException();
            return RenderMethod(clusters);
        }

        private Bitmap RenderMethod(List<FractalCluster> clusters)
        {

            var g = System.Drawing.Graphics.FromImage(_bitmap);
            DrawFractal(g, clusters);

            return _bitmap;
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
