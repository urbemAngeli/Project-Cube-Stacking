using System;
using UnityEngine;

namespace ExtraAssets.Scripts.Player
{
    [Serializable]
    public class WallCollision
    {
        private const float CheckDistance = 0.75f;
        private readonly Vector3 Direction = Vector3.forward;

        [SerializeField]
        private LayerMask _layerMask;

        private CubeHolder _cubeHolder;
        private Vector3 _origin;
        private Action _failCallback;
        
        private readonly Vector3[] _cornerPoints = new []
        {
            new Vector3(0.5f, 0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0)
        };

        public void Construct(CubeHolder cubeHolder, Action failCallback)
        {
            _cubeHolder = cubeHolder;
            _failCallback = failCallback;
        }

        public void Hit()
        {
            CheckCollision();
            CheckFail();
        }

        private void CheckCollision()
        {
            for (int i = 0; i < _cubeHolder.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _origin = _cubeHolder[i] + _cornerPoints[j] * 0.98f;
                
                    if (IsEnemyWall())
                    {
                        _cubeHolder.Detach(i);
                        i--;
                        
                        break;
                    }
                }
            }
        }

        private bool IsEnemyWall()
        {
            if (Physics.Raycast(_origin, Direction, CheckDistance, _layerMask))
            {
                Debug.DrawRay(_origin, Direction * CheckDistance, Color.red, 5);
                return true;
            }
            
            Debug.DrawRay(_origin, Direction * CheckDistance, Color.green, 5);
            return false;
        }

        private void CheckFail()
        {
            if(_cubeHolder.Count == 0)
                _failCallback?.Invoke();
        }
    }
}