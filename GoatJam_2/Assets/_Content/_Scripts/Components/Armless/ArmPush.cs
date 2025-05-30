using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using YInput;

namespace Yaygun.Components.Armless
{
    public enum EPushState{none, preparetion, pushing, recovery}
    public class ArmPush : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private Transform _arm1;
        [SerializeField] private float _pushForce;
        [SerializeField] private ForceMode2D _forceMode;

        [SerializeField] private Image _fillImage;
        
        [FoldoutGroup("Setting"), SerializeField] 
        private float _maxPushTime;
        
        [FoldoutGroup("Anim"), SerializeField] 
        private float _pushArmDistance;
        [FoldoutGroup("Anim"), SerializeField] 
        private float _recoverTime;
        [FoldoutGroup("Anim"), SerializeField] 
        private ParentFollower parentFollower;
        
        private EPushState _pushState;

        private bool _shouldPush;

        private float _pushingTime;
        
        void Update()
        {
            switch (_pushState)
            {
                case EPushState.none:
                    if (InputHandler.Instance.LeftClick.IsPressed)
                        StartPreperation();
                    break;
                case EPushState.preparetion:
                    PreperationState();
                    break;
                case EPushState.recovery:
                    break;
            }
        }

        private void StartPreperation()
        {
            _pushState = EPushState.preparetion;
            _pushingTime = 0;
        }

        private void PreperationState()
        {
            if (InputHandler.Instance.LeftClick.IsHeld)
            {
                _pushingTime += Time.deltaTime;
                _fillImage.fillAmount = _pushingTime / _maxPushTime;
                
                if (_pushingTime >= _maxPushTime)
                {
                    _pushingTime = _maxPushTime;
                    _shouldPush = true;
                }
            }
            else
                _shouldPush = true;
        }

        private void FixedUpdate()
        {
            if (_shouldPush)
            {
                _shouldPush = false;
                Push();
            }
        }

        private async void Push()
        {
            _pushState = EPushState.pushing;
            
            parentFollower.SetShouldFollow(false);
            
            _body.AddForce(_arm1.right *( -1 * _pushForce * ( _pushingTime / _maxPushTime)), _forceMode );
            
            parentFollower.PlayPushAnim(_pushArmDistance);
            
            AsyncPush();
        }

        [Button]
        private void PushTest()
        {
            _body.AddForce(_arm1.right *( -1 * _pushForce * ( 1)), _forceMode );

        }

        private async void AsyncPush()
        {
            await UniTask.WaitForSeconds(_recoverTime);
            
            parentFollower.SetShouldFollow(true);
            
            _fillImage.fillAmount = 0;
            
            _pushingTime = 0;
            _pushState = EPushState.none;
        }
    }
}
