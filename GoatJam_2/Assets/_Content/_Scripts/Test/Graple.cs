using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Yaygun.Controllers.Legless;
using Yaygun.Interfaces;
using YInput;

namespace Yaygun
{
    public enum EGrapleState{unable, none, prep,slide}
    public class Graple : MonoBehaviour
    {
        private EGrapleState _grapleState = EGrapleState.none;

        [FoldoutGroup("Referances"), SerializeField]
        private Rigidbody2D _rb;
        [FoldoutGroup("Referances"), SerializeField]
        private DistanceJoint2D _joint;
        [FoldoutGroup("Referances"), SerializeField]
        private LeglessController _controller;
        
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
        [FoldoutGroup("Settings"), SerializeField]
        private float _mapGrapableDistance;
        [FoldoutGroup("Settings"), SerializeField]
        private float _necExtraDistanceForEnterSlide;

        [FoldoutGroup("Settings/Horizontal"), SerializeField]
        private float _horizontalForce;


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
                    CheckForExit();
                    break;
                case EGrapleState.slide:
                    CheckForExit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void CheckForExit()
        {
            if(!InputHandler.Instance.Graple.IsHeld)
                EndGrapling();
        }
        private void EndGrapling()
        {
            _joint.enabled = false;
            _grapleState = EGrapleState.none;
        }

        private void EnterSlide()
        {
            _grapleState = EGrapleState.slide;
        }
        private void Prep()
        {
            _joint.distance = Mathf.SmoothDamp(_joint.distance, _slideDistance, ref _smoothVelocity, _smoothTime);
            if(_necExtraDistanceForEnterSlide + _slideDistance > _joint.distance)
                EnterSlide();
                
        }

        private void StartPrep()
        {
            ConnectToJoint();
            _grapleState = EGrapleState.prep;
            
        }

        private void ConnectToJoint()
        {
            Vector2 vel = _rb.linearVelocity;

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
                _controller.TryDisconnectFromSlime();
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

            RaycastHit2D hit = Physics2D.Raycast(pointA, direction, _mapGrapableDistance, _layerMask);

            if (hit.collider != null)
                if (hit.collider.TryGetComponent(out IGrapable theGrapable))
                {
                    grapable = theGrapable;
                    grapPosition = hit.point;
                    return true;
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

        private void FixedUpdate()
        {
            switch (_grapleState)
            {
                case EGrapleState.unable:
                    break;
                case EGrapleState.none:
                    break;
                case EGrapleState.prep:
                    break;
                case EGrapleState.slide:
                    _rb.AddForce(Vector2.right * (_horizontalForce * InputHandler.Instance.HorizontalMove * Time.fixedDeltaTime), ForceMode2D.Force);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
