using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Yaygun.Controllers.Legless;
using Yaygun.Interfaces;

namespace Yaygun.Components
{
    
    public class Fan : MonoBehaviour
    {
        private List<IPushable> _pushables = new List<IPushable>();

        [BoxGroup("Settings")]
        [BoxGroup("Settings/Speed"), SerializeField] 
        private float _desiredSpeed;

        [BoxGroup("Settings/Time"), SerializeField] 
        private float _smoothTime = 1;
        
        private float[] _velocities = new float[3];

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IPushable pushable))
            {
                _pushables.Add(pushable);
                _velocities[_pushables.Count - 1] = 0;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IPushable pushable))
            {
                _pushables.Remove(pushable);
            }
        }

        private void FixedUpdate()
        {
            int i = 0;
            foreach (var VARIABLE in _pushables)
            {
                float ySpeed = Mathf.SmoothDamp(VARIABLE.RB.linearVelocity.y, _desiredSpeed,
                    ref _velocities[i], _smoothTime);
                
                VARIABLE.RB.linearVelocity = new Vector2(VARIABLE.RB.linearVelocity.x, ySpeed);
                i++;
            }
        }
    }
}
