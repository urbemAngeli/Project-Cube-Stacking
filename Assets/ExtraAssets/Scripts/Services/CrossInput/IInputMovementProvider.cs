using UnityEngine;

namespace ExtraAssets.Scripts.Services.CrossInput
{
    public interface IInputMovementProvider
    {
        void StartMoving();
        void StopMoving();
        void SetDelta(Vector2 delta);
    }
}