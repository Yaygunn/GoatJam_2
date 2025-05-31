using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Yaygun
{
    public class PushUI : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _foreground;

        [SerializeField] private float _fadeInTime;
        [SerializeField] private float _fadeOutTime;
        [SerializeField] private Ease _ease = Ease.InOutCirc;
        
        Tween _tween1;
        Tween _tween2;

        private void Start()
        {
            _tween1 = _background.DOFade(0, _fadeInTime);
            _tween2 = _foreground.DOFade(0, _fadeInTime);
        }

        public void StartPush()
        {
            _tween1?.Kill();
            _tween2?.Kill();
            
            _tween1 = _background.DOFade(1, _fadeInTime)
                .SetEase(_ease);
            _tween2 = _foreground.DOFade(1, _fadeInTime)
                .SetEase(_ease);
        }

        public void Stop()
        {
            _tween1?.Kill();
            _tween2?.Kill();
            
            _tween1 = _background.DOFade(0, _fadeOutTime)
                .SetEase(_ease);
            _tween2 = _foreground.DOFade(0, _fadeOutTime)
                .SetEase(_ease);
        }

        public void FillRate(float fillAmount)
        {
            _foreground.fillAmount = fillAmount;
        }
        
    }
}
