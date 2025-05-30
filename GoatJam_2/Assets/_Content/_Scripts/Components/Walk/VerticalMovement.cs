using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yaygun.Components.Walk
{
    public enum EJumpState{none, prepare, ascend, top, descend}

    public class VerticalMovement : MonoBehaviour
    {

        [SerializeField] private float _jumpSpeed = 3;

        [FoldoutGroup("GravitySettings"), SerializeField]
        private SJumpGravity _ascentCheck;
        [FoldoutGroup("GravitySettings"), SerializeField]
        private SJumpGravity _topCheck;
        [FoldoutGroup("GravitySettings"), SerializeField]
        private SJumpGravity _descentCheck;
        
        private EJumpState _jumpState;
        
        private GroundChecker _groundChecker;
        private CharacterController _cc;

        private float _ySpeed;
        private float _gravity = 10;
        
        private void Start()
        {
            _cc = GetComponent<CharacterController>();
            _groundChecker = GetComponent<GroundChecker>();
        }

        private void Update()
        {
            if (_groundChecker.IsGrounded && _ySpeed < 0)
                _ySpeed = -0.1f;
            else 
                _ySpeed -= _gravity * Time.deltaTime;
            
            _cc.Move(Vector3.up * (_ySpeed * Time.deltaTime));
        }

        public void Jump()
        {
            if(_jumpState != EJumpState.none)
                return;
            if(_groundChecker.IsGrounded)
                _ySpeed = _jumpSpeed;
            //_jumpState = EJumpState.ascend;
        }

        /*private void DecideGravity()
        {
            switch (_jumpState)
            {
                case EJumpState.none:
                    break;
                case EJumpState.prepare:
                    break;
                case EJumpState.ascend:
                    if (_rb.linearVelocity.y < _ascentCheck.NecSpeed)
                    {
                        _rb.gravityScale = _ascentCheck.GravityScale;
                        _jumpState = EJumpState.top;
                    }
                    break;
                case EJumpState.top:
                    if (_rb.linearVelocity.y < _topCheck.NecSpeed)
                    {
                        _rb.gravityScale = _topCheck.GravityScale;
                        _jumpState = EJumpState.descend;
                    }
                    break;
                case EJumpState.descend:
                    if (_rb.linearVelocity.y > _descentCheck.NecSpeed)
                    {
                        _rb.gravityScale = _descentCheck.GravityScale;
                        _jumpState = EJumpState.none;
                    }
                    break;
                default:
                    break;
            }
        }*/
    }
    
    [System.Serializable]
    public struct SJumpGravity
    {
        [field: SerializeField] public float NecSpeed {get; private set;}
        [field: SerializeField] public float GravityScale {get; private set;}
    }
}
