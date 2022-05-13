using System.Collections.Generic;
using ExtraAssets.Scripts.Other;
using UnityEngine;

namespace ExtraAssets.Scripts.Infrastructure.Processors.Tick
{
    public class TickProcessor : MonoBehaviour, ITickProcessor
    {
        private List<ITickable> _ticks = new List<ITickable>();

        public void Add(ITickable tick)
        {
            if (_ticks.Contains(tick))
                return;

            _ticks.Add(tick);

            RegisterDisposeEvent(tick);
        }

        public void Remove(ITickable tick)
        {
            if(IsNotContainsTick(tick))
                return;
                
            _ticks.Remove(tick);
            
            UnregisterDisposeEvent(tick);
        }

        private void Update()
        {
            for (int i = 0; i < _ticks.Count; i++)
                _ticks[i].Tick();
        }

        private void RegisterDisposeEvent(ITickable tick)
        {
            if (tick is IDisposable disposable)
                disposable.OnDisposed += DisposeTick;
        }

        private void UnregisterDisposeEvent(ITickable tick)
        {
            if (tick is IDisposable disposable)
                disposable.OnDisposed -= DisposeTick;
        }

        private void DisposeTick(object sender) => 
            Remove(sender as ITickable);

        private bool IsNotContainsTick(ITickable tick) => 
            !_ticks.Contains(tick);
    }
}