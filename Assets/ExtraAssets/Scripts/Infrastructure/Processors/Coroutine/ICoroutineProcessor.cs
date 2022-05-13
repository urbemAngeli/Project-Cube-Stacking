using System.Collections;
using ExtraAssets.Scripts.Infrastructure.Services;

namespace ExtraAssets.Scripts.Infrastructure.Processors.Coroutine
{
    public interface ICoroutineProcessor : IService
    {
        IEnumerator Run(IEnumerator routine);
        IEnumerator Stop(IEnumerator routine);
        void StopAll();
        bool Contains(IEnumerator routine);
    }
}