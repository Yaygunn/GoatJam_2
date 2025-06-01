using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Video;

namespace Yaygun
{
    public class YVideoPlay : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        [SerializeField] private GameObject _disableAtEnd;

        private bool _shouldStart;

        private void Start()
        {
            _disableAtEnd.SetActive(false);
        }

        private void Update()
        {
            if(!_shouldStart)
                return;
            _shouldStart = false;
            _videoPlayer.Play();
        }

        [Button]
        public async UniTask Play()
        {
            _disableAtEnd.SetActive(true);
            await UniTask.WaitForEndOfFrame();
            _shouldStart = true;
            await UniTask.WaitForSeconds(1f);
            await UniTask.WaitWhile(()=>_videoPlayer.isPlaying);
            //_disableAtEnd.SetActive(false);
        }
    }
}
