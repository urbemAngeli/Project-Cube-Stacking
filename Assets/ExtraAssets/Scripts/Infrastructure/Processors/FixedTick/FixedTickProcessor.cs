using System.Collections.Generic;
using ExtraAssets.Scripts.Other;
using UnityEngine;

namespace ExtraAssets.Scripts.Infrastructure.Processors.FixedTick
{
    public class FixedTickProcessor : MonoBehaviour, IFixedTickProcessor
    {
        private List<IFixedTickable> _fixedTicks = new List<IFixedTickable>();

        public void Add(IFixedTickable fixedTick)
        {
            if (_fixedTicks.Contains(fixedTick))
                return;

            _fixedTicks.Add(fixedTick);

            RegisterDisposeEvent(fixedTick);
        }

        public void Remove(IFixedTickable fixedTick)
        {
            if(IsNotContainsTick(fixedTick))
                return;
                
            _fixedTicks.Remove(fixedTick);
            
            UnregisterDisposeEvent(fixedTick);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedTicks.Count; i++)
                _fixedTicks[i].FixedTick();
        }

        private void RegisterDisposeEvent(IFixedTickable fixedTick)
        {
            if (fixedTick is IDisposable disposable)
                disposable.OnDisposed += DisposeTick;
        }

        private void UnregisterDisposeEvent(IFixedTickable fixedTick)
        {
            if (fixedTick is IDisposable disposable)
                disposable.OnDisposed -= DisposeTick;
        }

        private void DisposeTick(object sender) => 
            Remove(sender as IFixedTickable);

        private bool IsNotContainsTick(IFixedTickable fixedTick) => 
            !_fixedTicks.Contains(fixedTick);
    }
}