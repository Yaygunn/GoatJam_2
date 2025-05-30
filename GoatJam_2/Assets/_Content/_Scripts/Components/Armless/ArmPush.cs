using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using YInput;

namespace Yaygun.Components.Armless
{
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


        private bool _startedArmPush;

        private bool _shouldPush;

        private float _pushingTime;
        
        void Update()
        {
            if (InputHandler.Instance.LeftClick.IsPressed)
            {
                if(_startedArmPush)
                    return;
                _startedArmPush = true;
                _pushingTime = 0;
            }
            
            if(InputHandler.Instance.LeftClick.IsHeld)
                _pushingTime += Time.deltaTime;
            
            if(InputHandler.Instance.LeftClick.IsReleased)
                _shouldPush = true;
            
            _fillImage.fillAmount = _pushingTime / _maxPushTime;
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
            parentFollower.SetShouldFollow(false);
            
            _body.AddForce(_arm1.right *( -1 * _pushForce * (_pushingTime / _maxPushTime)), _forceMode );
            
            parentFollower.PlayPushAnim(_pushArmDistance);
            
            AsyncPush();
        }

        private async void AsyncPush()
        {
            await UniTask.WaitForSeconds(_recoverTime);
            
            parentFollower.SetShouldFollow(true);
            
            _startedArmPush = false;
            _pushingTime = 0;
        }
    }
}
