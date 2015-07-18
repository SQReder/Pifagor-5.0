using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Pifagor.Geometry;

namespace Pifagor.ClusterTree
{
    public struct Range
    {
        public int From;
        public int To;
    }

    public class FractalWorker
    {
        private readonly Thread _thread;
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly object _lock = new object();
        private readonly CachedFractal _cachedFractal;
        private readonly ConcurrentQueue<Range> _rangesToProcess = new ConcurrentQueue<Range>();

        public FractalWorker(FractalCluster cluster)
        {
            _cachedFractal = new CachedFractal(cluster);
            _thread = new Thread(DoWork);
            _thread.Start();
        }

        public void EnqueueRange(Range range)
        {
            _rangesToProcess.Enqueue(range);
            _resetEvent.Reset();
        }

        private void DoWork()
        {
            while (_resetEvent.WaitOne())
            {
                Range range;
                var success =_rangesToProcess.TryDequeue(out range);
                if (success)
                {
                    ProcessRange(range);
                }
                _resetEvent.Set();
            }
        }

        private void ProcessRange(Range range)
        {
        }
    }
}
