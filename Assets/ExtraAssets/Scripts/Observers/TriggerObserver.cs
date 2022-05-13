using System;
using UnityEngine;

namespace ExtraAssets.Scripts.Observers
{
    public class TriggerObserver : MonoBehaviour
    {
        private event Action<Collider> OnEntered;
        private event Action<Collider> OnExited;

        public void AddListenerOnEnter(Action<Collider> callback) => 
            OnEntered += callback;

        public void AddListenerOnExit(Action<Collider> callback) => 
            OnExited += callback;

        public void RemoveListenerOnEnter(Action<Collider> callback) => 
            OnEntered -= callback;

        public void RemoveListenerOnExit(Action<Collider> callback) => 
            OnExited -= callback;

        private void OnTriggerEnter(Collider other) => 
            OnEntered?.Invoke(other);

        private void OnTriggerExit(Collider other) => 
            OnExited?.Invoke(other);
    }
}