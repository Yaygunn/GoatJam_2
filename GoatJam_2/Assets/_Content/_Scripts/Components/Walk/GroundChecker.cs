using Sirenix.OdinInspector;
using UnityEngine;

namespace Yaygun.Components.Walk
{
    public class GroundChecker : MonoBehaviour
    {
        [FoldoutGroup("Settings"), SerializeField]
        private LayerMask _groundLayers;
        [FoldoutGroup("Settings"), SerializeField]
        private float _radius = 0.45f;
        [FoldoutGroup("Settings"), SerializeField]
        private float _yOffset = 0.4f;
        private readonly Collider[] _hitColliders = new Collider[1];

        [field:SerializeField,ReadOnly]
        public bool IsGrounded { get; private set; }

        private void Update() => CheckIfGrounded();

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 position = new(transform.position.x, transform.position.y + _yOffset, transform.position.z);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, _radius);
        }
        #endif

        private void CheckIfGrounded()
        {
            Vector3 position = new(transform.position.x, transform.position.y + _yOffset, transform.position.z);
            IsGrounded = Physics.OverlapSphereNonAlloc(position, _radius, _hitColliders, _groundLayers) > 0;
        }
    }
}