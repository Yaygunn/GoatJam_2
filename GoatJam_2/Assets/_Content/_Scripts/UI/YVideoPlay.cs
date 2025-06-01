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
            _disableAtEnd.SetActive(true);
            _videoPlayer.Play();
            await UniTask.WaitForSeconds(1f);
            await UniTask.WaitWhile(()=>_videoPlayer.isPlaying);
            _disableAtEnd.SetActive(false);
        }
    }
}
