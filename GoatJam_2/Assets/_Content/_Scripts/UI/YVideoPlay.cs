using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace Yaygun
{
    public class YVideoPlay : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        [SerializeField] private GameObject _disableAtEnd;


        public async UniTask Play()
        {
            _videoPlayer.Play();
            _disableAtEnd.SetActive(true);
            UniTask.WaitUntil(()=>!_videoPlayer.isPlaying);
            _disableAtEnd.SetActive(false);
        }
    }
}
