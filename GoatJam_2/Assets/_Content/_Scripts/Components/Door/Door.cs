using System;
using By2m.Systems;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = System.Object;

namespace Yaygun
{
    public class Door : MonoBehaviour
    {
        private bool _isOpen;
        
        [SerializeField] private Animator _animator;
        private const string _doorHashOpen = "Open";
        
        public const float AnimTime = 0.5f;

        [SerializeField] private GameObject _colliderParent;

        [Button]
        public async void OpenDoor()
        {
            if(_isOpen)
                    return;
            
            _isOpen = true;
            _animator.SetBool(_doorHashOpen, true);
            
            AudioPlay.DoorOpen();
            
            await UniTask.WaitForSeconds(AnimTime);
            
            ResetDoorCollider();
        }

        [Button]
        public void CloseDoor()
        {
            if(!_isOpen)
                return;
            _isOpen = false;
            _animator.SetBool(_doorHashOpen, false);

            AudioPlay.DoorClose();
            ResetDoorCollider();
        }

        private void ResetDoorCollider()
        {
            _colliderParent.SetActive(!_isOpen);
        }
    }
}
