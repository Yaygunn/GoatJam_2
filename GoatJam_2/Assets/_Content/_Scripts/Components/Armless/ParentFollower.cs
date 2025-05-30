using UnityEngine;

namespace Yaygun.Components.Armless
{
    public class ParentFollower : MonoBehaviour
    {
        [SerializeField] private float _smoothPosTime = 0.2f;
        [SerializeField] private float _smoothRotTime = 0.2f;
        
        private Vector3 _positionVelocity;

        private float _rotationVelocity;
        
        private Transform _parent;

        private bool _shouldFollow = true;
        
        void Start()
        {
            _parent = transform.parent;
            transform.SetParent(null);
        }

        public void SetShouldFollow(bool shouldFollow)
        {
            _shouldFollow = shouldFollow;
            if (_shouldFollow)
            {
                transform.position = _parent.position;
                transform.rotation = _parent.rotation;
            }
        }

        public void PlayPushAnim(float pushArmDistance)
        {
            transform.position += transform.right  * pushArmDistance;
        }

        void Update()
        {
            if(!_shouldFollow)
                return;
            
            PosOperation();
            RotOperation();
        }

        private void PosOperation()
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                _parent.position,
                ref _positionVelocity,
                _smoothPosTime
            );
        }
        
        private void RotOperation()
        {
            float currentZ = transform.eulerAngles.z;

            float targetZ = _parent.eulerAngles.z;

            float deltaAngle = Mathf.DeltaAngle(currentZ, targetZ);

            float smoothedDelta = Mathf.SmoothDampAngle(0, deltaAngle, ref _rotationVelocity, _smoothRotTime);

            float newZ = currentZ + smoothedDelta;
            transform.rotation = Quaternion.Euler(0, 0, newZ);
        }
    }
}
