using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Pifagor.ClusterTree;
using Pifagor.Geometry;

namespace SQReder.Pifagor
{
    public partial class Form1 : Form
    {
        private FractalCluster _cluster;
        private readonly List<FractalCluster> _clusters = new List<FractalCluster>();
        private int count;

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

            Populate(_cluster, 4);
        }

        private void Populate(FractalCluster cluster, int d)
        {
            _clusters.Clear();
            var treeBase = cluster.Count;
            var lastIndex = ClusterMath.GetFirstIndexOfLayer(treeBase, d + 1);
            for (var i = 0; i != lastIndex; ++i)
            {
                var pathToIndex = ClusterMath.GetPathToIndex(treeBase, i);
                _clusters.Add(Transform(cluster, pathToIndex));
            }
        }

        private FractalCluster Transform(FractalCluster cl, int[] pathToIndex)
        {
            var c = cl;
            for (int i = 0; i != pathToIndex.Length; ++i)
            {
                var idx = pathToIndex[i];
                var segment = cl[idx];
                c = c.TransformWith(segment);
            }
            return c;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(SystemBrushes.Control, new Rectangle(0, 0, 1000, 1000));
            foreach (var cluster in _clusters)
            {
                foreach (var segment in cluster)
                {
                    DrawSegment(e.Graphics, segment, 100);
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
            count++;
            Populate(_cluster, count);
            Invalidate();
        }
    }
}
