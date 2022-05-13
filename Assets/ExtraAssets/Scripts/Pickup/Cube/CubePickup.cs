using ExtraAssets.Scripts.Map.Cube;
using ExtraAssets.Scripts.Observers;
using UnityEngine;

namespace ExtraAssets.Scripts.Pickup.Cube
{
    public class CubePickup : MonoBehaviour
    {
        public bool IsContainsCube => _cubeObject != null;
        
        [SerializeField]
        private TriggerObserver _triggerObserver;
        
        private DetectionTarget _detectionTarget = new DetectionTarget();

        private CubeObject _cubeObject;

        public void SetCube(CubeObject cubeObject)
        {
            Construct(cubeObject);
            Initialize();
        }

        public CubeObject RemoveCube()
        {
            CubeObject cubeObject = _cubeObject;
            _cubeObject = null;
            
            Dispose();
            
            return cubeObject;
        }

        private void Construct(CubeObject cubeObject)
        {
            _cubeObject = cubeObject;
            _detectionTarget.Construct(_triggerObserver, cubeObject);
        }

        private void Initialize()
        {
            InitializeCubeObject();
            
            _detectionTarget.Initialize();

            _detectionTarget.OnDetached += OnCubePickuped;
        }

        private void Dispose()
        {
            _detectionTarget.Dispose();
            
            _detectionTarget.OnDetached -= OnCubePickuped;
        }

        private void InitializeCubeObject()
        {
            _cubeObject.Rigidbody.isKinematic = true;
            _cubeObject.BoxCollider.enabled = false;

            _cubeObject.transform.parent = transform;
            
            _cubeObject.transform.position = transform.position;
            _cubeObject.transform.rotation = Quaternion.identity;
        }

        private void OnCubePickuped()
        {
            _cubeObject = null;
            
            Dispose();
        }
    }
}