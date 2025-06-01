using System;
using By2m.Systems;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yaygun
{
    public class PressurePad : MonoBehaviour
    {
        [SerializeField] private float _moveTime;
        [SerializeField] private Transform _pad;
        [SerializeField] private Vector3 _moveDistance;
        
        LevelController _levelController;

        private int _triggered;
        
        Tween _tween;

        private Vector3 _startPos;

        private bool _loadedNextLevel;
        
        private void Start()
        {
            _levelController = GetComponentInParent<LevelController>();
            _startPos = _pad.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _triggered++;
            if(_triggered == 1)
                Pressed();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _triggered--;
            if(_triggered == 0)
                DeActivated();
        }

        private void Pressed()
        {
            _tween?.Kill();

            print("open");

            _tween = _pad.DOMove(_startPos - _moveDistance, _moveTime)
                .SetEase(Ease.InOutCirc)
                .OnComplete(LoadNextLevel);
        }
        
        private void DeActivated()
        {
            _tween?.Kill();

            print("close");

            
            _tween = _pad.DOMove(_startPos , _moveTime)
                .SetEase(Ease.InOutCirc)
                .OnComplete(DeleteNextLevel);
            
        }

        private void LoadNextLevel()
        {
            if(_loadedNextLevel)
                return;
            _loadedNextLevel = true;
            
            AudioPlay.ButtonOn();
            _levelController?.OpenNextRoomDoor();
        }

        private void DeleteNextLevel()
        {
            if(!_loadedNextLevel)
                return;
            _loadedNextLevel = false;
            AudioPlay.ButtonOf();

            _levelController?.CloseNextRoomDoor();
            
        }

        
    }
}
