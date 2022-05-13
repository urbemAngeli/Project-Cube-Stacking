using System;
using UnityEngine;

namespace ExtraAssets.Scripts.Observers
{
    public class CollisionObserver : MonoBehaviour
    {
        private event Action<Collision> OnEntered;
        private event Action<Collision> OnExited;

        public void AddListenerOnEnter(Action<Collision> callback) => 
            OnEntered += callback;

        public void AddListenerOnExit(Action<Collision> callback) => 
            OnExited += callback;

        public void RemoveListenerOnEnter(Action<Collision> callback) => 
            OnEntered -= callback;

        public void RemoveListenerOnExit(Action<Collision> callback) => 
            OnExited -= callback;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.collider.name);
            OnEntered?.Invoke(collision);
        }

        private void OnCollisionExit(Collision other) => 
            OnExited?.Invoke(other);
    }
}