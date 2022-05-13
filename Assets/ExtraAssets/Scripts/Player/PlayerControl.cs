using System;
using System.Collections.Generic;
using ExtraAssets.Scripts.Infrastructure.Processors.FixedTick;
using ExtraAssets.Scripts.Map.Cube;
using ExtraAssets.Scripts.Pickup.Cube;
using ExtraAssets.Scripts.Services.CrossInput;
using UnityEngine;

namespace ExtraAssets.Scripts.Player
{
    public class PlayerControl : MonoBehaviour, IDamageableByWall, IReceiveCubeObject, IFailable, IFixedTickable
    {
        public event Action OnFail;
        
        [SerializeField]
        private CubeHolder _cubeHolder;
        
        [SerializeField]
        private Movement _movement;

        [SerializeField]
        private WallCollision _wallCollision;

        [SerializeField]
        private Rigidbody _rigidbody;
        
        public void Construct(IInputService input)
        {
            _movement.Construct(_rigidbody, input);
            _wallCollision.Construct(_cubeHolder, RunFail);
        }

        public void Initialize() => 
            _movement.Initialize();

        public void Set(Vector3 position, CubeObject baseObject)
        {
            _rigidbody.position = position;
            
            AttachCubeObject(baseObject);
            
            _movement.StartMoving();
        }

        public void Dispose() => 
            _movement.Dispose();

        public void FixedTick() => 
            _movement.FixedTick();

        public void HitWithWall() => 
            _wallCollision.Hit();

        public void AttachCubeObject(CubeObject cubeObject) => 
            _cubeHolder.Attach(cubeObject);

        public List<CubeObject> DetachAllCubeObjects() => 
            _cubeHolder.DetachAll();

        private void RunFail() => 
            OnFail?.Invoke();
    }
}