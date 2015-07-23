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
        /// <summary>
        /// Размеры изображения-буфера
        /// </summary>
        private Size _imageBufferSize;

        readonly Segment _scaleSegment = new Segment(new Vector(650, 0), new Vector(750, 0));
        readonly RandomColorGenerator _colorGenerator = new RandomColorGenerator();

        /// <summary>
        /// Количество кластеров на котрые бьется задание рендера для параллелизации
        /// </summary>
        private const int ClusterBatchSize = 16;

        private readonly object _lock = new object();
        private Bitmap _lastFullRenderedBitmap;

        public Bitmap LastFullRenderedResult 
        {
            get { return _lastFullRenderedBitmap; }
            private set
            {
                lock (_lock)
                {
                    _lastFullRenderedBitmap = value;
                }
            }
        }

        /// <summary>
        /// Создает новый объект
        /// </summary>
        /// <param name="imageBufferSize">Размеры изображения-буфера</param>
        public RenderEngine(Size imageBufferSize)
        {
            _imageBufferSize = imageBufferSize;
            _lastFullRenderedBitmap = new Bitmap(1, 1);
        }

        /// <summary>
        /// Асинхронно рендерит кластеры
        /// </summary>
        /// <param name="token">Токен отмены</param>
        /// <param name="clusters">Кластеры для рендера</param>
        /// <returns>Задачу рендера</returns>
        public Task RenderAsync(CancellationToken token, List<FractalCluster> clusters)
        {
            return Task.Run(() => RenderMethod(token, clusters), token);
        }
        
        private void RenderMethod(CancellationToken token, List<FractalCluster> clusters)
        {

            var bitmap = new Bitmap(_imageBufferSize.Width, _imageBufferSize.Height);
            var g = System.Drawing.Graphics.FromImage(bitmap);

            var drawLock = new object();

            var ranges = ClusterMath.MakeRanges(clusters.Count, ClusterBatchSize);
            try
            {
                ranges.AsParallel()
                    .AsOrdered()
                    .WithCancellation(token)
                    .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                    .Select(r => PartialDraw(clusters.GetRange(r.Begin, r.Count)))
                    .ForAll(b =>
                    {
                        lock (drawLock)
                        {
                            g.DrawImageUnscaled(b.Item1, b.Item2);
                        }
                    });

                _lastFullRenderedBitmap = bitmap;
            }
            catch (OperationCanceledException e)
            {
                _lastFullRenderedBitmap = new Bitmap(1,1);
                throw;
            }
        }

        /// <summary>
        /// Рендерит кластеры в отдельном изображении
        /// </summary>
        /// <param name="clusters">Кластеры для рендера</param>
        /// <returns>Изображение на котором отрендерены кластеры</returns>
        /// todo reduce buffer image size
        private Tuple<Bitmap, Point> PartialDraw(IEnumerable<FractalCluster> clusters)
        {
            var scaledClusters = clusters.AsParallel().Select(c => c.TransformWith(_scaleSegment)).ToList();

            var rect = ClusterMath.ClustersClipRectangle(scaledClusters);
            var seg = new Segment(rect.Left, rect.Top, rect.Right, rect.Bottom);
            var offset = new Size(rect.Left, rect.Top);
            var offsetSegment = new Segment(-offset.Width, -offset.Height, -offset.Width + 1, -offset.Height);
            var shiftedSegments = scaledClusters.AsParallel().Select(c => c.TransformWith(offsetSegment)).ToList();
            var width = (int) (seg.End.X - seg.Begin.X);
            var height = (int) (seg.End.Y - seg.Begin.Y);
            var bitmap = new Bitmap(width, height);
            var graphics = System.Drawing.Graphics.FromImage(bitmap);

            DrawFractal(graphics, Pens.Black, shiftedSegments);
            //var pen = new Pen(_colorGenerator.RandomColor());
            //graphics.DrawRectangle(pen, 0, 0, bitmap.Width - 1, bitmap.Height - 1);

            return new Tuple<Bitmap, Point>(bitmap, new Point(offset.Width, offset.Height));
        }

        /// <summary>
        /// Рендерит кластеры на конкретной поверхности
        /// </summary>
        /// <param name="g">Экземпляр Graphics</param>
        /// <param name="pen"></param>
        /// <param name="clusters">Кластеры для рендера</param>
        /// <param name="offset"></param>
        private void DrawFractal(System.Drawing.Graphics g, Pen pen, IEnumerable<FractalCluster> clusters)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;

            foreach (var cluster in clusters)
            {
                var fc = cluster;
                fc.Draw(g, pen);
            }
        }
    }

    public class RandomColorGenerator
    {
        readonly Random r = new Random(DateTime.Now.Millisecond);

        public Color RandomColor()
        {
            byte red = (byte)r.Next(0, 255);
            byte green = (byte)r.Next(0, 255);
            byte blue = (byte)r.Next(0, 255);

            return Color.FromArgb(128, red, green, blue);
        }
    }
}
