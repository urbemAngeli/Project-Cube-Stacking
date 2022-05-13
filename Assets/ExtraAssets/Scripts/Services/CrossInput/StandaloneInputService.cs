using System;
using ExtraAssets.Scripts.Infrastructure.Processors.Tick;
using UnityEngine;

namespace ExtraAssets.Scripts.Services.CrossInput
{
    public class StandaloneInputService : IInputService, IInputMovementProvider
    {
        public event Action OnMovingStarted;
        public event Action OnMovingEnded;

        public Vector2 Delta => _delta;

        private Vector2 _delta;

        public void StartMoving() => 
            OnMovingStarted?.Invoke();

        public void StopMoving() => 
            OnMovingEnded?.Invoke();

        public void SetDelta(Vector2 delta) => 
            _delta = delta;
    }
}