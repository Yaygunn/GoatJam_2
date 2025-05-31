using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaygun.Controllers.Legless;

namespace Yaygun
{
    public class Slime : MonoBehaviour
    {
        [field:SerializeField]
        public Transform AttachPoint;
        
        private float _resetTime = 0.3f;

        private bool _canConnect = true;
        
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out LeglessController leglessController))
                if (_canConnect)
                    leglessController.TryAttachSlime(this);
        }

        public void Disconnect()
        {
            _canConnect = false;
            StartCoroutine(StartCor());
        }

        private IEnumerator StartCor()
        {
            yield return new WaitForSeconds(_resetTime);
            _canConnect = true;
        }
    }
}
