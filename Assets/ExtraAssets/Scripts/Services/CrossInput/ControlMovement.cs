using UnityEngine;
using UnityEngine.EventSystems;

namespace ExtraAssets.Scripts.Services.CrossInput
{
    public class ControlMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private IInputMovementProvider _inputMovementProvider;

        public void Construct(IInputMovementProvider inputMovementProvider) => 
            _inputMovementProvider = inputMovementProvider;

        public void OnBeginDrag(PointerEventData eventData) => 
            _inputMovementProvider.StartMoving();

        public void OnDrag(PointerEventData eventData) => 
            _inputMovementProvider.SetDelta(eventData.delta);

        public void OnEndDrag(PointerEventData eventData) => 
            _inputMovementProvider.StopMoving();
    }
}