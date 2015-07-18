using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Pifagor.Geometry;

namespace Pifagor.Graphics
{
    public static class RenderEngine
    {
        public static Task<Bitmap> Render(List<FractalCluster> clusters)
        {
            return Task<Bitmap>.Run(() => RenderMethod(clusters));
        }

        private static Bitmap RenderMethod(List<FractalCluster> clusters)
        {
            var bitmap = new Bitmap(1000, 1000);

            var g = System.Drawing.Graphics.FromImage(bitmap);
            DrawFractal(g, clusters);

            return bitmap;
        }

        private static void DrawFractal(System.Drawing.Graphics g, List<FractalCluster> clusters)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;

            //g.FillRectangle(SystemBrushes.Control, DisplayRectangle); // disabled for no size available

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
