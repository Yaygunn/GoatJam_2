using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using YInput;

namespace Yaygun.Components.Armless
{
    public class ArmPush : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private Transform _arm1;
        [SerializeField] private float _pushForce;
        [SerializeField] private ForceMode2D _forceMode;

        [FoldoutGroup("Anim"), SerializeField] 
        private float _pushArmDistance;
        [FoldoutGroup("Anim"), SerializeField] 
        private float _recoverTime;
        [FoldoutGroup("Anim"), SerializeField] 
        private ParentFollower parentFollower;


        private bool _shouldPush;
        
        void Update()
        {
            if(InputHandler.Instance.LeftClick.IsPressed)
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
            parentFollower.SetShouldFollow(false);
            
            _body.AddForce(_arm1.right * -1 * _pushForce, _forceMode );
            
            parentFollower.PlayPushAnim(_pushArmDistance);
            
            AsyncPush();
        }

        private async void AsyncPush()
        {
            await UniTask.WaitForSeconds(_recoverTime);
            
            parentFollower.SetShouldFollow(true);
        }
    }
}
