using ExtraAssets.Scripts.Observers;
using UnityEngine;

namespace ExtraAssets.Scripts.Zones.Grilling
{
    public class GrillingZone : MonoBehaviour
    {
        [SerializeField]
        private TriggerObserver _triggerObserver;

        private DetectionTarget _detectionTarget = new DetectionTarget();

        private void Awake() => 
            Construct();

        private void Start() => 
            Initialize();

        private void Construct() => 
            _detectionTarget.Construct(_triggerObserver);

        private void Initialize() => 
            _detectionTarget.Initialize();
    }
}