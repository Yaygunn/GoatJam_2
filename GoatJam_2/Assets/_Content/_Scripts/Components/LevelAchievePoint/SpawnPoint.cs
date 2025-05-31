using System;
using UnityEngine;
using Yaygun.Controllers.Legless;
using Yaygun.Controllers.Walk;

namespace Yaygun
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _child;

        private void Start()
        {
            if(_child)
                Destroy(_child);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out LeglessController legless))
                Achieve();
            if(other.TryGetComponent(out WalkController walk))
                Achieve();
               
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out LeglessController legless))
                Achieve();
            if(other.TryGetComponent(out WalkController walk))
                Achieve();
        }

        private void Achieve()
        {
            LevelController levelController = GetComponentInParent<LevelController>();
            if(levelController)
                levelController.EnteredLevelArea();
            else 
                Debug.LogError("Cant find levelcontroller");
            Destroy(gameObject);
        }
    }
}
