using System;
using UnityEngine;
using Yaygun.Interfaces;

namespace Yaygun
{
    public class Box : MonoBehaviour,  IPushable
    {
        public Rigidbody2D RB {get; private set;}

        private void OnEnable()
        {
            RB = GetComponent<Rigidbody2D>();
        }
    }
}
