using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Yaygun.Systems;

namespace Yaygun.UI
{
    public class VolumeSettingsPanel : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField]
        private SVolumeSlider[] _volumeSliders;


        private void Start()
        {
            foreach (SVolumeSlider volumeSlider in _volumeSliders)
            {
                volumeSlider.Slider.SetValueWithoutNotify(SaveSystem.VolumeSettings.GetVolume(volumeSlider.VolumeType));               
                
                volumeSlider.Slider.onValueChanged.AddListener(val =>
                    OnSliderValueChanged(val, volumeSlider.VolumeType));
            }
        }

        private void OnSliderValueChanged(float val, EVolumeType volType)
        {
            SaveSystem.VolumeSettings.SetVolume(volType, val);
        }
    }

    [Serializable]
    public struct SVolumeSlider
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private EVolumeType _volumeType;

        public Slider Slider => _slider;
        public EVolumeType VolumeType => _volumeType;
    }
}