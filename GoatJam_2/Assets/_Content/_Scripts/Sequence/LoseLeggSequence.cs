using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YInput;
using UnityEngine.UI;
using UnityEngine.Video;
using Yaygun.Controllers.Walk;
using Yaygun.Managers;

namespace Yaygun
{
    public class LoseLeggSequence : MonoBehaviour
    {
        [SerializeField] private float _firstDelay;

        [SerializeField] private Image _spriteAnimImage;
        
        [SerializeField] private YVideoPlay _videoPlay;

        [SerializeField] private GameObject _pressEText;

        private bool _isInTriggerZone;

        private bool _hasActivated;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
                if(_isInTriggerZone)
                    if(!_hasActivated)
                        PlaySequence();
        }

        private void OnTriggerEnter(Collider other)
        {
            _pressEText?.SetActive(true);
            _isInTriggerZone = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _isInTriggerZone = false;
            _pressEText?.SetActive(false);
        }


        private async void PlaySequence()
        {
            
            Destroy(_pressEText);
            
            InputHandler.Instance.Pause();

            await ChopOfLegs();
            
            await UniTask.WaitForSeconds(_firstDelay);
            
            await SpriteAnim.PlayAnim(_spriteAnimImage);
            
            await _videoPlay.Play();

            SpawnNextLevel();
            
            
            await SpriteAnim.PlayAnim(_spriteAnimImage, -1);
            
            InputHandler.Instance.Resume();
        }

        private void SpawnNextLevel()
        {
            transform.SetParent(null);
            Destroy((FindAnyObjectByType<WalkController>().gameObject));
            LevelManager.Instance.DeleteAllLoadedAndLoadNextLevel();
        }

        private async UniTask ChopOfLegs()
        {
            WalkController _controller = FindAnyObjectByType<WalkController>();
            
            print("chop of leggs");
        }
    }
}
