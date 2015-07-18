using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pifagor.ClusterTree;
using Pifagor.Geometry;
using Pifagor.Graphics;

namespace SQReder.Pifagor
{
    public partial class Form1 : Form
    {
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

            _fractal = new CachedFractal(cluster);
        }

        private void DrawFractalBuffered(Bitmap bitmap)
        {
            var context = BufferedGraphicsManager.Current;
            var myBuffer = context.Allocate(CreateGraphics(), DisplayRectangle);

            var g = myBuffer.Graphics;
            g.FillRectangle(SystemBrushes.Control, DisplayRectangle);
            g.DrawImageUnscaled(bitmap, 0, 0);

            myBuffer.Render();
            myBuffer.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _count++;

            var cts = new CancellationTokenSource();
            var renderEngine = new RenderEngine(DisplayRectangle.Size);

            var clusters = _fractal.ProcessLevels(_count);
            renderEngine.StartRender();
            renderEngine.Render(clusters);
            renderEngine.EndRender();
            DrawFractalBuffered(renderEngine.Result);
        }
    }
}
