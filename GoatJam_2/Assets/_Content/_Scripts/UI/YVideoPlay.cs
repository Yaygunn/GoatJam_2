using UnityEngine;
using UnityEngine.Video;

namespace Yaygun
{
    public class YVideoPlay : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        void Start()
        {
            _videoPlayer.Play();
        }

        
        void Update()
        {
            if (!_videoPlayer.isPlaying)
            {
                Time.timeScale = 0;
                Destroy(gameObject);
            }
        }
    }
}
