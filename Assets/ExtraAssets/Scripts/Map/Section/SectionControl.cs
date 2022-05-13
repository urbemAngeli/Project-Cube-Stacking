using System.Collections.Generic;
using ExtraAssets.Scripts.Infrastructure.Processors.FixedTick;
using ExtraAssets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace ExtraAssets.Scripts.Map.Section
{
    public class SectionControl : MonoBehaviour, IFixedTickable, IService
    {
        [SerializeField]
        private Transform _holderRoot;
        
        [SerializeField]
        private SectionMovement _movement;

        [SerializeField]
        private SectionHolder _holder;

        public void Construct()
        {
            _holder.Construct(_holderRoot);
            _movement.Construct(_holder);
        }

        public void FixedTick() => 
            _movement.FixedTick();

        public void StartMoving() => 
            _movement.Start();

        public void StopMoving() => 
            _movement.Stop();

        public void Attach(TrackSection trackSection) => 
            _holder.Attach(trackSection);

        public List<TrackSection> DetachAll() => 
            _holder.DetachAll();
    }
}