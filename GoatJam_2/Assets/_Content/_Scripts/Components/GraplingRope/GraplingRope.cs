using System;
using DG.Tweening;
using UnityEngine;

namespace Yaygun
{
    public class GraplingRope : MonoBehaviour
    {
        [SerializeField] LineRenderer _lineRenderer;

        [SerializeField] private Transform _destination;

        [SerializeField] private float _ropeSped;
        
        Tween _tween;

        private void Update()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _destination.position);
        }

        public float GoToDestination(Vector3 destination)
        {
            _tween?.Kill();

            float reachTime = Vector3.Distance(destination, transform.position) / _ropeSped;
            _tween = _destination.DOMove(destination, reachTime);
            return reachTime;
        }

        public void GoBackHand()
        {
            _tween?.Kill();
            
            _tween = _destination.DOLocalMove(Vector3.zero, Vector3.Distance( Vector3.zero, transform.localPosition)/ _ropeSped);
        }
    }
}
