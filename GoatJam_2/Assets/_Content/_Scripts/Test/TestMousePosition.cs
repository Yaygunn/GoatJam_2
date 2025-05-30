using UnityEngine;

namespace Yaygun
{
    public class TestMousePosition : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;

        [SerializeField] private Transform _playerPosShover;

        [SerializeField] private Transform _arm1;
        [SerializeField] private Transform _arm2;
        
        void FixedUpdate()
        {
            Vector3 mousePos = GetMousePositionOnPlane();
            
            
            _arm1.GetComponent<Rigidbody2D>().SetRotation(_arm1.Get2DLookAngle(mousePos));//Look2D(mousePos);
            _arm2.Look2D(mousePos);
        }

        private Vector3 GetMousePositionOnPlane()
        {
            Vector3 planeNormal = Vector3.Cross(_playerBody.right, _playerBody.up).normalized;
            Vector3 planePoint = _playerBody.position; 

            Plane customPlane = new Plane(planeNormal, planePoint);
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (customPlane.Raycast(mouseRay, out float enter))
                return mouseRay.GetPoint(enter);
            
            Debug.LogError("Mouse position is not on the plane");
            return Vector3.zero;
        }
    }
}
