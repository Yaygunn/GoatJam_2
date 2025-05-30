using UnityEngine;

namespace Yaygun
{
    public class TestModelFollow : MonoBehaviour
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
            transform.position = Vector3.SmoothDamp(
                transform.position,
                _parent.position,
                ref _positionVelocity,
                _smoothPosTime
            );
            
            /*float smootedValue = Mathf.SmoothDamp(transform.eulerAngles.z, _parent.eulerAngles.z, ref _rotationVelocity, _smoothRotTime);
            transform.rotation = Quaternion.EulerAngles(0,0, smootedValue);*/
            RotOperation();
        }
        
        private void RotOperation()
        {
            // Current z rotation
            float currentZ = transform.eulerAngles.z;

            // Target z rotation
            float targetZ = _parent.eulerAngles.z;

            // Calculate shortest difference between angles (handles wrap-around)
            float deltaAngle = Mathf.DeltaAngle(currentZ, targetZ);

            // Smooth damp the delta angle from 0 to deltaAngle
            float smoothedDelta = Mathf.SmoothDampAngle(0, deltaAngle, ref _rotationVelocity, _smoothRotTime);

            // Apply the smoothed rotation
            float newZ = currentZ + smoothedDelta;
            transform.rotation = Quaternion.Euler(0, 0, newZ);
        }
    }
}
