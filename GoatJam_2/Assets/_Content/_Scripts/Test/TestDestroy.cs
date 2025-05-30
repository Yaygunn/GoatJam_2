using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Yaygun
{
    public class TestDestroy : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCode;

        [SerializeField] private GameObject _go;
        private void Update()
        {
            if(Input.GetKeyDown(_keyCode))
                Destroy(_go);
                
        }
    }
}
