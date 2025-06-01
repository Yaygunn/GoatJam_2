using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Yaygun.Components.Armless;
using Yaygun.Interfaces;

namespace Yaygun.Controllers.Legless
{
    public enum ELeglessState{normal, slimed}
    public class LeglessController : MonoBehaviour, IPushable
    {
        [field:FoldoutGroup("Components"), SerializeField]
        public ArmPush CArmPush { get; private set; }
        [field:FoldoutGroup("Components"), SerializeField]
        public Graple CGraple { get; private set; }
        public Rigidbody2D RB { get; private set; }
        
        
        public Slime Slime {get; private set;}
        
        private ELeglessState _leglessState = ELeglessState.normal;

        private void OnEnable()
        {
            RB = GetComponent<Rigidbody2D>();
        }

        public bool TryAttachSlime(Slime slime)
        {
            _leglessState = ELeglessState.slimed;
            Slime = slime;
            
            RB.linearVelocity = Vector2.zero;
            RB.isKinematic = true;

            transform.DOMove(slime.AttachPoint.position, 0.2f).SetEase(Ease.InOutSine);
            transform.SetParent(Slime.AttachPoint.transform);
            return true;
        }

        public void TryDisconnectFromSlime()
        {
            Slime?.Disconnect();
            RB.isKinematic = false;
            _leglessState = ELeglessState.normal;
            transform.SetParent(null);
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
        }
    }
}
