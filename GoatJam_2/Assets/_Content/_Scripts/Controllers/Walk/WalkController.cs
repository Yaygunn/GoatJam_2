using System;
using UnityEngine;
using Yaygun.Components.Walk;
using YInput;

namespace Yaygun.Controllers.Walk
{
    public class WalkController : MonoBehaviour
    {
        public Movement CMovement { get; private set; }
        public VerticalMovement CVerticalMovement { get; private set; }

        private void OnEnable()
        {
            CMovement = GetComponent<Movement>();
            CVerticalMovement = GetComponent<VerticalMovement>();
        }

        private void Update()
        {
            CMovement.HorizontalMove(InputHandler.Instance.HorizontalMove);
            if (InputHandler.Instance.Jump.IsPressed)
                CVerticalMovement.Jump();
        }
    }
}
