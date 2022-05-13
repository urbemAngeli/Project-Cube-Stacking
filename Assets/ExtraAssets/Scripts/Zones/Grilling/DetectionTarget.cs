using ExtraAssets.Scripts.Observers;
using UnityEngine;

namespace ExtraAssets.Scripts.Zones.Grilling
{
    public class DetectionTarget
    {
        private TriggerObserver _triggerObserver;

        public void Construct(TriggerObserver triggerObserver)
        {
            _triggerObserver = triggerObserver;
        }

        public void Initialize()
        {
            _triggerObserver.AddListenerOnEnter(CheckTarget);
        }

        private void CheckTarget(Collider collider)
        {
            if (collider.TryGetComponent<IGrillable>(out IGrillable grillable)) 
                grillable.Grill();
        }
    }
}