using System.Collections.Generic;
using UnityEngine;

namespace Yaygun.Systems
{
    public enum EVolumeType{master, music, sfx}

    public class VolumeSettings
    {
        private const string _volumeSaveString = "VolumeSave";
        private const float _initialVolume = 1;

        private Dictionary<EVolumeType, float> _volumes = new();

        public VolumeSettings()
        {
            if(PlayerPrefs.HasKey(GetVolumeSavePath(EVolumeType.master)))
                LoadVolumes();
            else
                InitializeVolumes();
        }
        
        public float GetVolume(EVolumeType volumeType)
        {
            return _volumes[volumeType];
        }

        public void SetVolume(EVolumeType volumeType, float volume)
        {
            _volumes[volumeType] = volume;
            Debug.Log("Set volume here");
            SaveVolume(volumeType);
        }

        private void LoadVolumes()
        {
            foreach (var volumeType in GetVolumeTypesAsArray())
                 SetVolume(volumeType,PlayerPrefs.GetFloat(GetVolumeSavePath(volumeType)));
        }

        private void InitializeVolumes()
        {
            foreach (var volumeType in GetVolumeTypesAsArray())
                SetVolume(volumeType, _initialVolume);
        }

        private void SaveVolume(EVolumeType volumeType)
        {
            PlayerPrefs.SetFloat(GetVolumeSavePath(volumeType), _volumes[volumeType]);
        }

        private string GetVolumeSavePath(EVolumeType volumeType)
        {
            return _volumeSaveString + volumeType.ToString();
        }

        private EVolumeType[] GetVolumeTypesAsArray()
        {
            return new EVolumeType[] { EVolumeType.master, EVolumeType.music, EVolumeType.sfx };
        }
        
        private string GetVolumePath(EVolumeType VolumeType)
        {
            return VolumeType switch
            {
                EVolumeType.master => "vca:/Master",
                EVolumeType.music => "vca:/Music",
                EVolumeType.sfx => "vca:/SFX",
                _ => "vca:/Master"
            };
        }
    }
}
