using System;

namespace Pifagor.ClusterTree
{
    /// <summary>
    /// ������������� ���������� �������� �� ��������� ����� Dispose
    /// ������� ����������, ���� Dispose ��� ��������� ��� ����� �������
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
        /// ��������� ������������ ����������� ������, ��������� � �������������� ��� ������� ������������� ��������.
        /// ����� ������������ �������� �� ��������� ����� Dispose � ������� ����������, ���� ������� ��� ���� �����������
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