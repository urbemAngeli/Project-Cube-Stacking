using System.Collections.Generic;
using ExtraAssets.Scripts.Other;
using UnityEngine;

namespace ExtraAssets.Scripts.Infrastructure.Processors.LateTick
{
    public class LateTickProcessor : MonoBehaviour, ILateTickProcessor
    {
        private List<ILateTickable> _lateTicks = new List<ILateTickable>();

        public void Add(ILateTickable lateTick)
        {
            if (_lateTicks.Contains(lateTick))
                return;

            _lateTicks.Add(lateTick);

            RegisterDisposeEvent(lateTick);
        }

        public void Remove(ILateTickable lateTick)
        {
            if(IsNotContainsTick(lateTick))
                return;
                
            _lateTicks.Remove(lateTick);
            
            UnregisterDisposeEvent(lateTick);
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _lateTicks.Count; i++)
                _lateTicks[i].LateTick();
        }

        private void RegisterDisposeEvent(ILateTickable lateTick)
        {
            if (lateTick is IDisposable disposable)
                disposable.OnDisposed += DisposeTick;
        }

        private void UnregisterDisposeEvent(ILateTickable lateTick)
        {
            if (lateTick is IDisposable disposable)
                disposable.OnDisposed -= DisposeTick;
        }

        private void DisposeTick(object sender) => 
            Remove(sender as ILateTickable);

        private bool IsNotContainsTick(ILateTickable lateTick) => 
            !_lateTicks.Contains(lateTick);
    }
}