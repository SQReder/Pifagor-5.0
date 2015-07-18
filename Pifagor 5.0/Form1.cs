using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Pifagor.ClusterTree;
using Pifagor.Geometry;

namespace SQReder.Pifagor
{
    public partial class Form1 : Form
    {
        private List<FractalCluster> _clusters = new List<FractalCluster>();
        private int _count;
        private CachedFractal _fractal;

        public Form1()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var cluster = new FractalCluster
            {
                Segments = { 
                    new Segment(new Vector(0.5, 1.5), new Vector(1, 1)),
                    new Segment(new Vector(0, 1), new Vector(0.5, 1.5))
                },
                Decore =
                {
                    new Segment(new Vector(0,0), new Vector(0,1)),
                    new Segment(new Vector(1,0), new Vector(1,1)),
                    new Segment(new Vector(0,1), new Vector(1,1)),
                }
            };

            var fw = new FractalWorker(cluster);
            _fractal = new CachedFractal(cluster);
        }

        private void DrawFractalBuffered(List<FractalCluster> clusters)
        {
            var context = BufferedGraphicsManager.Current;
            var myBuffer = context.Allocate(CreateGraphics(), DisplayRectangle);

            DrawFractal(myBuffer.Graphics, clusters);

            myBuffer.Render();
            myBuffer.Dispose();
        }

        protected void DrawFractal(Graphics g, List<FractalCluster> clusters)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.FillRectangle(SystemBrushes.Control, DisplayRectangle);

            // scale segment
            var seg = new Segment(new Vector(650, 0), new Vector(750, 0));
            foreach (var cluster in clusters)
            {
                var fc = cluster.TransformWith(seg);
                fc.Draw(g, Pens.Black);
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _count++;
            _fractal.ProcessLevels(_count).ContinueWith(task =>
            {
                _clusters = task.Result;
                DrawFractalBuffered(_clusters);
            });
        }
    }
}
