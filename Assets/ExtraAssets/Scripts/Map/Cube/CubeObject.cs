using System;
using ExtraAssets.Scripts.Infrastructure;
using ExtraAssets.Scripts.Zones.Grilling;
using UnityEngine;

namespace ExtraAssets.Scripts.Map.Cube
{
    public class CubeObject : MonoBehaviour, IGrillable
    {
        public event Action<CubeObject> OnGrilled; 

        public BoxCollider BoxCollider => _boxCollider;
        public Rigidbody Rigidbody => _rigidbody;

        [SerializeField]
        private BoxCollider _boxCollider;

        [SerializeField]
        private Rigidbody _rigidbody;
        
        private PoolMono<CubeObject> _pool;
        
        public void Initialize() => 
            gameObject.SetActive(true);

        public void Dispose() => 
            gameObject.SetActive(false);

        public void Grill() => 
            OnGrilled?.Invoke(this);
    }
}