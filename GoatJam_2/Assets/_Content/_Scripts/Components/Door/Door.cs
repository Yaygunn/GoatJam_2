using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yaygun
{
    [RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
    public class Door : MonoBehaviour
    {
        private bool _isOpen;
        
        private Animator _animator;
        private BoxCollider2D _collider;
        
        private const string _doorHashOpen = "Open";
        
        public const float AnimTime = 1;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
        }

        public async void OpenDoor()
        {
            if(_isOpen)
                    return;
            
            _animator.SetBool(_doorHashOpen, true);
            _isOpen = true;
            
            await UniTask.WaitForSeconds(AnimTime);
            
            ResetDoorCollider();
        }

        public void CloseDoor()
        {
            if(!_isOpen)
                return;
            _isOpen = false;
            ResetDoorCollider();
            _animator.SetBool(_doorHashOpen, false);
        }

        private void ResetDoorCollider()
        {
            _collider.enabled = !_isOpen;
        }
    }
}
