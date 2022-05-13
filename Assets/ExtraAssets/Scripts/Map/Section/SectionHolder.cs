using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraAssets.Scripts.Map.Section
{
    [Serializable]
    public class SectionHolder
    {
        private const int Distance = 30;
        
        public int Count => _trackSections.Count;

        [SerializeField]
        private List<TrackSection> _trackSections = new List<TrackSection>();
        private Transform _root;
        private Vector3 _position;

        public TrackSection this[int index] => 
            _trackSections[index];

        public void Construct(Transform root) => 
            _root = root;

        public void Attach(TrackSection section)
        {
            float zOffset = CalculateOffset();
            _position = new Vector3(0, 0, zOffset);

            section.Set(_root, _position);
            
            _trackSections.Add(section);
            
            section.OnGrilled += Detach;
        }

        public List<TrackSection> DetachAll()
        {
            List<TrackSection> detachedSections = new List<TrackSection>();

            for (int i = 0; i < _trackSections.Count; i++)
            {
                detachedSections.Add(_trackSections[i]);
                Detach(_trackSections[i]);
                i--;
            }

            return detachedSections;
        }

        private float CalculateOffset()
        {
            return _trackSections.Count == 0
                ? -Distance
                : _trackSections[_trackSections.Count - 1].PhysicPosition.z + Distance;
        }

        private void Detach(TrackSection section)
        {
            _trackSections.Remove(section);
            
            section.OnGrilled -= Detach;
        }
    }
}