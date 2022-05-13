using System;
using System.Collections.Generic;
using ExtraAssets.Scripts.Map.Cube;
using UnityEngine;

namespace ExtraAssets.Scripts.Player
{
    [Serializable]
    public class CubeHolder : MonoBehaviour
    {
        private const float Offset = 1;

        public int Count => _cubeObjects.Count;
        
        [SerializeField]
        private List<CubeObject> _cubeObjects = new List<CubeObject>();
        
        private RigidbodyConstraints _attachedConstraints = RigidbodyConstraints.FreezeRotationX 
                                                            | RigidbodyConstraints.FreezeRotationY
                                                            | RigidbodyConstraints.FreezeRotationZ 
                                                            | RigidbodyConstraints.FreezePositionX
                                                            | RigidbodyConstraints.FreezePositionZ;

        public Vector3 this[int index] => 
            _cubeObjects[index].Rigidbody.position;

        public void Attach(CubeObject cubeObject)
        {
            Rigidbody rigidbody = cubeObject.Rigidbody;
            
            Vector3 position = CalculateCubePosition();
            rigidbody.transform.position = position;
            
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            
            rigidbody.constraints = _attachedConstraints;
            
            rigidbody.transform.parent = transform;
            
            _cubeObjects.Add(cubeObject);
        }

        public void Detach(int index)
        {
            Rigidbody rigidbody = _cubeObjects[index].Rigidbody;
            
            rigidbody.transform.parent = null;
            
            rigidbody.constraints =
                RigidbodyConstraints.FreezeRotationX 
                | RigidbodyConstraints.FreezeRotationY
                | RigidbodyConstraints.FreezeRotationZ 
                | RigidbodyConstraints.FreezePositionX;
            
            _cubeObjects.RemoveAt(index);
        }

        private Vector3 CalculateCubePosition()
        {
            if (_cubeObjects.Count == 0)
                return transform.position;

            return _cubeObjects[_cubeObjects.Count - 1].Rigidbody.position + Vector3.up * Offset;
        }

        public List<CubeObject> DetachAll()
        {
            List<CubeObject> detachedCubes = new List<CubeObject>();

            for (int i = 0; i < _cubeObjects.Count; i++)
            {
                detachedCubes.Add(_cubeObjects[i]);
                
                Detach(i);
                i--;
            }

            return detachedCubes;
        }
    }
}