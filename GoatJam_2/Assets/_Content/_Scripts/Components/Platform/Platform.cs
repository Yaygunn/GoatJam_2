using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yaygun.Components
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Vector2 _destination;
        
        [SerializeField] private float _moveTime;
        
        [SerializeField] private float _waitTime;
        
        [SerializeField] private Ease _ease;

        [SerializeField] private Rigidbody2D _followerRb;
        
        
        private float _currentTime;
        
        Tween _moveTween;
        
        private Vector2 _startPos;
        
        private Vector2 _lastDestination;


        private void Start()
        {
            _startPos = transform.position;
            _destination += _startPos;
            _lastDestination = _startPos;
            _followerRb.transform.SetParent(transform.parent);
            MoveToNextDestination();
        }

        private void FixedUpdate()
        {
            _followerRb?.MovePosition(transform.position);
        }

        private async void Wait()
        {
            await UniTask.WaitForSeconds(_waitTime);
            
            if(gameObject.activeInHierarchy)
                MoveToNextDestination();
        }

        private void MoveToNextDestination()
        {
            _moveTween?.Kill();

            if (_lastDestination == _startPos)
                _lastDestination = _destination;
            else
                _lastDestination = _startPos;
            
            transform.DOMove(_lastDestination, _moveTime).SetEase(_ease)
                .OnComplete(Wait);
        }

        private void OnDisable()
        {
            _moveTween?.Kill();
        }


        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 position = new(transform.position.x + _destination.x, transform.position.y + _destination.y, transform.position.z);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, 0.3f);
        }
        #endif
    }
}
