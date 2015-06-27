using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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
                new Segment(new Vector(0.5, 1.5), new Vector(1, 1)),
                new Segment(new Vector(0, 1), new Vector(0.5, 1.5))
            };

            _fractal = new CachedFractal(cluster);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillRectangle(SystemBrushes.Control, new Rectangle(0, 0, 1000, 1000));
            var seg = new Segment(new Vector(500, 0), new Vector(600, 0));
            foreach (var cluster in _clusters)
            {
                var fc = cluster.TransformWith(seg);
                fc.Draw(g, Pens.Black);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _count++;
            _clusters = _fractal.ProcessLevels(_count).ToList();
            Invalidate();
        }
    }
}
