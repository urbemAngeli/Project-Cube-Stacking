using ExtraAssets.Scripts.Observers;
using ExtraAssets.Scripts.Player;
using UnityEngine;

namespace ExtraAssets.Scripts.Enemy
{
    public class DetectionTarget
    {
        private TriggerObserver _triggerObserver;
        private GameObject _collider;

        public void Construct(TriggerObserver triggerObserver, GameObject collider)
        {
            _triggerObserver = triggerObserver;
            _collider = collider;
        }

        public void Initialize() => 
            _triggerObserver.AddListenerOnEnter(CheckTarget);

        private void CheckTarget(Collider collider)
        {
            if (collider.TryGetComponent<IDamageableByWall>(out IDamageableByWall damageable))
            {
                _triggerObserver.RemoveListenerOnEnter(CheckTarget);
                _collider.SetActive(true);
                
                damageable.HitWithWall();
            }
        }
    }
}