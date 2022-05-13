using System;
using ExtraAssets.Scripts.Map.Cube;
using ExtraAssets.Scripts.Observers;
using UnityEngine;

namespace ExtraAssets.Scripts.Pickup.Cube
{
    public class DetectionTarget
    {
        public event Action OnDetached;
        
        private TriggerObserver _triggerObserver;
        private CubeObject _cubeObject;

        public void Construct(TriggerObserver triggerObserver, CubeObject cubeObject)
        {
            _triggerObserver = triggerObserver;
            _cubeObject = cubeObject;
        }

        public void Initialize() => 
            _triggerObserver.AddListenerOnEnter(CheckTarget);

        public void Dispose() => 
            _triggerObserver.RemoveListenerOnEnter(CheckTarget);

        private void CheckTarget(Collider collider)
        {
            if (collider.TryGetComponent(out IReceiveCubeObject receiver)) 
                DetachCube(receiver);
        }

        private void DetachCube(IReceiveCubeObject receiver)
        {
            _triggerObserver.RemoveListenerOnEnter(CheckTarget);

            _cubeObject.Rigidbody.transform.parent = null;
            _cubeObject.BoxCollider.enabled = true;
            _cubeObject.Rigidbody.isKinematic = false;

            receiver.AttachCubeObject(_cubeObject);
            
            OnDetached?.Invoke();
        }
    }
}