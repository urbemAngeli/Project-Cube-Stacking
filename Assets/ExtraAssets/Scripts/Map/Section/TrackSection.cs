using System;
using ExtraAssets.Scripts.Enemy;
using ExtraAssets.Scripts.Zones.Grilling;
using UnityEngine;

namespace ExtraAssets.Scripts.Map.Section
{
    public class TrackSection : MonoBehaviour, IGrillable
    {
        public event Action<TrackSection> OnGrilled;
        
        public Vector3 PhysicPosition => _rigidbody.position;
        
        [SerializeField]
        private GameObject _wallHinge;
        
        [SerializeField]
        private GameObject _cubePackHinge;
        
        [SerializeField]
        private Rigidbody _rigidbody;

        private EnemyWall _enemyWall;
        private CubePack _cubePack;

        public void Initialize() => 
            gameObject.SetActive(true);

        public void Dispose() => 
            gameObject.SetActive(false);

        public void AttachWall(EnemyWall enemyWall)
        {
            enemyWall.transform.parent = _wallHinge.transform;
            enemyWall.transform.localPosition = Vector3.zero;

            _enemyWall = enemyWall;
        }

        public void AttachCubePack(CubePack cubePack)
        {
            cubePack.transform.parent = _cubePackHinge.transform;
            cubePack.transform.localPosition = Vector3.zero;
            
            _cubePack = cubePack;
        }

        public bool TryDetachWall(out EnemyWall enemyWall)
        {
            enemyWall = _enemyWall;
            _enemyWall = null;
            return enemyWall != null;
        }
        
        public bool TryDetachCubePack(out CubePack cubePack)
        {
            cubePack = _cubePack;
            _cubePack = null;
            return cubePack != null;
        }

        public void Grill() => 
            OnGrilled?.Invoke(this);

        public void Move(Vector3 at)
        {
            transform.position = at;
            //_rigidbody.MovePosition(at);
        }

        public void Set(Transform root, Vector3 position)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            
            transform.parent = root;
            transform.position = position;
            _rigidbody.position = position;
        }

        public void ForceReset()
        {
            transform.parent = null;
            
            transform.position = Vector3.zero;
            _rigidbody.position = Vector3.zero;
            
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}