using ExtraAssets.Scripts.Pickup.Cube;
using UnityEngine;

namespace ExtraAssets.Scripts.Map
{
    public class CubePack : MonoBehaviour
    {
        public int Length => _cubes.Length;
        
        [SerializeField] 
        private CubePickup[] _cubes;

        private void OnValidate() => 
            _cubes = GetComponentsInChildren<CubePickup>();

        public CubePickup this[int index] =>
            _cubes[index];

        public void Initialize() => 
            gameObject.SetActive(true);

        public void Dispose() => 
            gameObject.SetActive(false);
    }
}