using System.Collections.Generic;

namespace Pifagor.Geometry
{
    public class TransformationChainBuilder
    {
        private readonly Queue<TransformationMatrix> _chain = new Queue<TransformationMatrix>();
        private TransformationMatrix _result = TransformationMatrix.Noop;
         
        public TransformationChainBuilder Append(TransformationMatrix tm)
        {
            _chain.Enqueue(tm);
            return this;
        }

        public TransformationMatrix Result
        {
            get
            {
                if (_chain.Count != 0)
                    Flatten();
                return _result;
            }
        }

        private void Flatten()
        {
            while (_chain.Count != 0)
            {
                var tm = _chain.Dequeue();
                _result *= tm;
            }
        }
    }
}
