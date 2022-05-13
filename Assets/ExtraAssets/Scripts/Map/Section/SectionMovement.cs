using System;
using UnityEngine;

namespace ExtraAssets.Scripts.Map.Section
{
    [Serializable]
    public class SectionMovement
    {
        [SerializeField]
        private float _speed;

        private SectionHolder _sectionHolder;
        
        private bool _isMoving;
        private TrackSection _section;
        private Vector3 _movement;

        public void Construct(SectionHolder sectionHolder) => 
            _sectionHolder = sectionHolder;

        public void Start() => 
            _isMoving = true;

        public void Stop() => 
            _isMoving = false;

        public void FixedTick()
        {
            if(_isMoving)
                Move();
        }
        
        private void Move()
        {
            for (int i = 0; i < _sectionHolder.Count; i++)
            {
                _section = _sectionHolder[i];
                
                _movement = _section.PhysicPosition + -Vector3.forward * _speed * Time.fixedDeltaTime;
                _section.Move(_movement);
            }
            
            _section = null;
        }
    }
}