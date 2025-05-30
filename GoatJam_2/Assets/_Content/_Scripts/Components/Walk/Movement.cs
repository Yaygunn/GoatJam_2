using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yaygun.Components.Walk
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 1;

        [SerializeField] private float _acceleration = 1;
        
        private float _currentSpeed;

        private CharacterController _cc;
        

        private void Start()
        {
            _cc = GetComponent<CharacterController>();
        }


        public void HorizontalMove(float moveInput)
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, moveInput * _maxSpeed, _acceleration * Time.deltaTime);
            _cc.Move(Vector3.right * _currentSpeed * Time.deltaTime);
        }

        
    }

    
}
