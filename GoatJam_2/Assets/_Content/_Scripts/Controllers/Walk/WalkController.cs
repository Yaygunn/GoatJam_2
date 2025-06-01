using System;
using UnityEngine;
using Yaygun.Components.Walk;
using Yaygun.Managers;
using YInput;

namespace Yaygun.Controllers.Walk
{
    public class WalkController : MonoBehaviour
    {
        public Movement CMovement { get; private set; }
        public VerticalMovement CVerticalMovement { get; private set; }

        [SerializeField] private GameObject[] _pawns;

        private void OnEnable()
        {
            CMovement = GetComponent<Movement>();
            CVerticalMovement = GetComponent<VerticalMovement>();
            CameraManager.Instance.FollowMe(transform);
        }

        private void Update()
        {
            CMovement.HorizontalMove(InputHandler.Instance.HorizontalMove);
            if (InputHandler.Instance.Jump.IsPressed)
                CVerticalMovement.Jump();
        }

        public void ChangePawns()
        {
            _pawns[0].SetActive(false);
            _pawns[1].SetActive(true);
        }
    }
}
