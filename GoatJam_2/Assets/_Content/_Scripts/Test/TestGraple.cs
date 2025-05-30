using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Yaygun.Interfaces;
using YInput;

namespace Yaygun
{
    public enum EGrapleState{unable, none, prep,slide}
    public class TestGraple : MonoBehaviour
    {
        private EGrapleState _grapleState = EGrapleState.none;

        [FoldoutGroup("Referances"), SerializeField]
        private Rigidbody2D _rb;
        [FoldoutGroup("Referances"), SerializeField]
        private DistanceJoint2D _joint;
        
        [FoldoutGroup("Settings"), SerializeField]
        private Transform _rayStartPos;
        [FoldoutGroup("Settings"), SerializeField]
        private LayerMask _layerMask;
        [FoldoutGroup("Settings"), SerializeField]
        private Vector2 _connectionAnchorOffset;
        [FoldoutGroup("Settings"), SerializeField]
        private float _slideDistance;
        [FoldoutGroup("Settings"), SerializeField]
        private float _smoothTime;

        [SerializeField] private GameObject _object;
        

        private float _smoothVelocity;
        

        private IGrapable _grapable;
        private Vector2 _grapPos;
        
        private void Update()
        {
            switch (_grapleState)
            {
                case EGrapleState.none:
                    if (InputHandler.Instance.Graple.IsPressed)
                        TryStartPrep();
                    break;
                case EGrapleState.prep:
                    Prep();
                    if(!InputHandler.Instance.Graple.IsHeld)
                        EndGrapling();
                    break;
                case EGrapleState.slide:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EndGrapling()
        {
            _joint.enabled = false;
            _grapleState = EGrapleState.none;
        }
        
        private void Prep()
        {
            _joint.distance = Mathf.SmoothDamp(_joint.distance, _slideDistance, ref _smoothVelocity, _smoothTime);
        }

        private void StartPrep()
        {
            Vector2 vel = _rb.linearVelocity;
            _grapleState = EGrapleState.prep;
            _joint.transform.position = _grapPos;
            _joint.connectedBody = _rb;
            _joint.connectedAnchor = _connectionAnchorOffset;
            _joint.enabled = true;
            _rb.linearVelocity = vel;
        }

        private void TryStartPrep()
        {
            Vector2 mousePos = GetMousePositionOnPlane();
            if (TryGetGrapable(mousePos, out IGrapable grapable, out Vector2 grapPosition))
            {
                _grapable = grapable;
                _grapPos = grapPosition;
                StartPrep();
            }
        }

        private bool TryGetGrapable(Vector2 fireTowardsPos, out IGrapable grapable, out Vector2 grapPosition)
        {
            grapable = null;
            grapPosition = Vector3.zero;
            
            Vector2 pointA = _rayStartPos.position;
            Vector2 pointB = fireTowardsPos;

            Vector2 direction = (pointB - pointA).normalized;
            float distance = Vector2.Distance(pointA, pointB);

            RaycastHit2D hit = Physics2D.Raycast(pointA, direction, distance, _layerMask);

            Debug.DrawLine(pointA, pointB, Color.red); // For visualization

            if (hit.collider != null)
            {
                _object = hit.collider.gameObject;
                if (hit.collider.TryGetComponent(out IGrapable theGrapable))
                {
                    grapable = theGrapable;
                    grapPosition = hit.point;
                    return true;
                }
            }

            return false;
        }
        
        private Vector3 GetMousePositionOnPlane()
        {
            Vector3 planeNormal = Vector3.Cross(_rb.transform.right, _rb.transform.up).normalized;
            Vector3 planePoint = _rb.transform.position; 

            Plane customPlane = new Plane(planeNormal, planePoint);
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (customPlane.Raycast(mouseRay, out float enter))
                return mouseRay.GetPoint(enter);
            
            Debug.LogError("Mouse position is not on the plane");
            return Vector3.zero;
        }
    }
}
