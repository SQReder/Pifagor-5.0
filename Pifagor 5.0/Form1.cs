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
        private FractalCluster _cluster;
        private List<FractalCluster> _clusters = new List<FractalCluster>();
        private int _count;

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
            _cluster = new FractalCluster
            {
                new Segment(new Vector(0.5, 1.5), new Vector(1, 1)),
                new Segment(new Vector(0, 1), new Vector(0.5, 1.5))
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillRectangle(SystemBrushes.Control, new Rectangle(0, 0, 1000, 1000));
            foreach (var cluster in _clusters)
            {
                foreach (var segment in cluster)
                {
                    DrawSegment(g, segment, 100);
                }
            }
        }

        private static void DrawSegment(Graphics g, Segment segment, int scale)
        {
            var size = new Size(500, 000);
            PointF begin = segment.Begin * scale;
            PointF end = segment.End * scale;

            var pen = new Pen(Color.Black);

            g.DrawLine(pen, begin + size, end + size);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _count++;
            _clusters = CachedMath.Populate(_cluster, _count).ToList();
            Invalidate();
        }
    }
}
