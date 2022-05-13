using System;
using ExtraAssets.Scripts.Services.CrossInput;
using UnityEngine;

namespace ExtraAssets.Scripts.Player
{
    [Serializable]
    public class Movement
    {
        [SerializeField]
        private float _speed = 4;
        
        [SerializeField]
        private float _maxOffset = 2;

        private Rigidbody _rigidbody;
        private IInputService _input;
        private bool _isMoving;

        public void Construct(Rigidbody rigidbody, IInputService input)
        {
            _rigidbody = rigidbody;
            _input = input;
        }

        public void Initialize()
        {
            _input.OnMovingStarted += StartMoving;
            _input.OnMovingEnded += StopMoving;
        }

        public void Dispose()
        {
            _input.OnMovingStarted -= StartMoving;
            _input.OnMovingEnded -= StopMoving;
        }

        public void StartMoving() => 
            _isMoving = true;

        private void StopMoving() => 
            _isMoving = false;

        public void FixedTick()
        {
            if (_isMoving)
                Move();
        }

        private void Move()
        {
            float xPosition = _rigidbody.position.x + _input.Delta.x * _speed * Time.fixedDeltaTime;
            xPosition = Mathf.Clamp(xPosition, -_maxOffset, _maxOffset);

            Vector3 movement = new Vector3(xPosition, _rigidbody.position.y, _rigidbody.position.z);
            
            _rigidbody.MovePosition(movement);
        }
    }
}