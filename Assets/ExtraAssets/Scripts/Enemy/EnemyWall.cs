using ExtraAssets.Scripts.Observers;
using UnityEngine;

namespace ExtraAssets.Scripts.Enemy
{
    public class EnemyWall : MonoBehaviour
    {
        [SerializeField]
        private GameObject _collider;

        [SerializeField]
        private TriggerObserver _triggerObserver;

        private DetectionTarget _detectionTarget = new DetectionTarget();
        
        public void Construct() => 
            _detectionTarget.Construct(_triggerObserver, _collider);

        public void Initialize()
        {
            _collider.gameObject.SetActive(false);
            
            gameObject.SetActive(true);

            _detectionTarget.Initialize();
        }

        public void Dispose() => 
            gameObject.SetActive(false);
    }
}