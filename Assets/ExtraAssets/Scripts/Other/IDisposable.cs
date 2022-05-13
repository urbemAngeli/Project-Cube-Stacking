using System;

namespace ExtraAssets.Scripts.Other
{
    public interface IDisposable
    {
        event Action<object> OnDisposed;
        void Dispose();
    }
}