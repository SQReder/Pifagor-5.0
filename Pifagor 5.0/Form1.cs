using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Pifagor.Geometry;

namespace SQReder.Pifagor
{
    public partial class Form1 : Form
    {
        private FractalCluster _cluster;
        private readonly List<FractalCluster> _clusters = new List<FractalCluster>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _cluster = new FractalCluster
            {
                new Segment(new Vector(0.5, 1.5), new Vector(1, 1)),
                new Segment(new Vector(0, 1), new Vector(0.5, 1.5))
            };

            _clusters.Add(_cluster);
            foreach (var segment in _cluster)
            {
                _clusters.Add(_cluster.TransformWith(segment));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var cluster in _clusters)
            {
                foreach (var segment in cluster)
                {
                    DrawSegment(e.Graphics, segment, 10);
                }
            }
        }

        private static void DrawSegment(Graphics g, Segment segment, int scale)
        {
            var size = new Size(100,100);
            PointF begin = segment.Begin*scale;
            PointF end = segment.End*scale;
            g.DrawLine(Pens.Black, begin + size, end + size);
        }
    }
}
