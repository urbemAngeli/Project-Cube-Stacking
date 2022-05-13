using UnityEngine;

namespace ExtraAssets.Scripts.GizmoHelpers
{
    public class DrawingCube : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _center = Vector3.zero;

        [SerializeField]
        private Vector3 _size = Vector3.one;

        [SerializeField]
        private Color _color = Color.magenta;

        private void OnDrawGizmos()
        {
            var oldMatrix = Gizmos.matrix;
            
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = _color;
            
            Gizmos.DrawCube(_center, _size);

            Gizmos.matrix = oldMatrix;
        }
    }
}