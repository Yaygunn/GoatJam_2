using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yaygun.Components.Walk
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 1;

        [SerializeField] private float _acceleration = 1;

        [SerializeField] private Transform _lavuk;
        
        private float _currentSpeed;

        private CharacterController _cc;
        
        private Quaternion _leftRotation;
        private Quaternion _rightRotation;
        

        private void Start()
        {
            _cc = GetComponent<CharacterController>();
            _rightRotation = _lavuk.rotation;
            _leftRotation = Quaternion.Euler(0, 180, 0) * _rightRotation;
        }


        public void HorizontalMove(float moveInput)
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, moveInput * _maxSpeed, _acceleration * Time.deltaTime);
            _cc.Move(Vector3.right * _currentSpeed * Time.deltaTime);
            Rotate();
        }

        private void Rotate()
        {
            if (_currentSpeed > 0)
                _lavuk.rotation = _rightRotation;
            else 
                _lavuk.rotation = _leftRotation;
        }

        
    }

    
}
