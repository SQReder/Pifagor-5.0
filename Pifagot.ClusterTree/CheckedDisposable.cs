using System;

namespace Pifagor.ClusterTree
{
    /// <summary>
    /// Предоставляет функционал проверки на повторный вызов Dispose
    /// Бросает исключение, если Dispose уже вызывался для этого объекта
    /// </summary>
    public abstract class CheckedDisposable : IDisposable
    {
        private bool _hasDisposed = false;
        private readonly object _disposeMutex = new object();

        private void CheckDispose()
        {
            if (_hasDisposed)
                throw new ObjectDisposedException("Object already disposed");
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
        /// Также осуществляет проверку на повторный вызов Dispose и бросает исключение, если ресурсы уже были освобождены
        /// </summary>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            lock (_disposeMutex)
            {
                CheckDispose();
                _hasDisposed = true;
            }
        }
    }
}