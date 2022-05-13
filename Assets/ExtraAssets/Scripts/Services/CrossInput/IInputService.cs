using System;
using ExtraAssets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace ExtraAssets.Scripts.Services.CrossInput
{
    public interface IInputService : IService
    {
        event Action OnMovingStarted;
        event Action OnMovingEnded;
        Vector2 Delta { get; }
    }
}