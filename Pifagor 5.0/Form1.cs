using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Pifagor.ClusterTree;
using Pifagor.Geometry;
using Pifagor.Graphics;

namespace Pifagor
{
    public partial class Form1 : Form
    {
        private int _count;
        private CachedFractal _fractal;
        private readonly RenderEngine _renderEngine;
        private CancellationTokenSource _cts;

        public Form1()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            _renderEngine = new RenderEngine(new Size(1920, 1080));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var cluster = new FractalCluster
            {
                Segments = {
                    //new Segment(new Vector(0.4, 1.5), new Vector(1, 1)),
                    //new Segment(new Vector(0, 1), new Vector(0.4, 1.5))
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

        private async void button1_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            _count++;

            var progress = new Progress<int>(i => this.Text = i.ToString());

            try
            {
                button1.Text = $"Process {_count}";
                var clusters = await _fractal.ProcessLevels(_cts.Token, _count);
                await _renderEngine.RenderAsync(_cts.Token, clusters);
                button1.Text = $"Finished {_count}";
            }
            catch (OperationCanceledException ex)
            {
                Text = ex.Message;
            }

            DrawFractalBuffered(_renderEngine.LastFullRenderedResult);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImageUnscaled(_renderEngine.LastFullRenderedResult, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _count = 0;
            button1_Click(null, null);
        }
    }
}
