using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Yaygun.Utilities.Singleton;

namespace By2m.Systems
{

    public enum VolumeType
    {
        Master,
        Music,
        SFX
    }

    public class AudioSystem : Singleton<AudioSystem>
    {
        private readonly Dictionary<EventReference, EventInstance> _activeEvents = new();
        private readonly Dictionary<GameObject, Dictionary<EventReference, EventInstance>> _keyedInstances = new();
        private readonly List<EventInstance> _oneShotInstances = new();


        [field:SerializeField] public AudioDataSO AudioData { get; private set; }

        protected override void Initialize()
        {
            AudioPlay.Initialize();
        }

        protected override void Destruction()
        {
            StopAllPersistentSounds();
            StopAllTrackedOneShots();
        }

        public float GetEventLength(EventReference sound)
        {
            if (sound.IsNull)
                return 0f;

            EventDescription eventDescription = RuntimeManager.GetEventDescription(sound);
            if (!eventDescription.isValid())
                return 0f;

            eventDescription.getLength(out int lengthMs);
            return lengthMs / 1000f;
        }

        #region Volume Control

        public void SetCurrentVolume(VolumeType volumeType, float volume)
        {
            string path = GetVolumePath(volumeType);
            VCA vca = RuntimeManager.GetVCA(path);
            volume = Mathf.Clamp01(volume);
            vca.setVolume(volume);
            SaveVolume(volumeType, volume);
        }

        public float GetCurrentVolume(VolumeType volumeType)
        {
            string path = GetVolumePath(volumeType);
            VCA vca = RuntimeManager.GetVCA(path);
            vca.getVolume(out float volume);
            return volume;
        }

        private string GetVolumePath(VolumeType volumeType)
        {
            return volumeType switch
            {
                VolumeType.Master => "vca:/Master",
                VolumeType.Music => "vca:/Music",
                VolumeType.SFX => "vca:/SFX",
                _ => "vca:/Master"
            };
        }

        private void SaveVolume(VolumeType volumeType, float volume)
        {
            PlayerPrefs.SetFloat($"{volumeType}Volume", volume);
            PlayerPrefs.Save();
        }

        private float LoadVolume(VolumeType volumeType)
        {
            string volumeKey = $"{volumeType}Volume";
            return PlayerPrefs.HasKey(volumeKey) ? PlayerPrefs.GetFloat(volumeKey) : 0.5f;
        }

        #endregion

        #region Play Music/SFX

        public void PlayOneShotAndForget(EventReference sound, Vector3 position = default)
        {
            if(sound.IsNull)
                return;
            RuntimeManager.PlayOneShot(sound, position);
        }

        /// <summary>
        ///     Currently for one shot we have param:
        /// </summary>
        public void PlayOneShotAndForget(EventReference sound, string parameterName, string parameterValue,
            Vector3 position = default
        )
        {
            EventInstance instance = RuntimeManager.CreateInstance(sound);
            instance.setParameterByNameWithLabel(parameterName, parameterValue);
            instance.start();
            instance.release();
        }

        public void PlayOneShotTracked(EventReference sound, Vector3 position = default)
        {
            EventInstance instance = RuntimeManager.CreateInstance(sound);
            instance.start();
            _oneShotInstances.Add(instance);
        }

        public void PlayOneShotTracked(EventReference sound, string parameterName, string parameterValue,
            Vector3 position = default
        )
        {
            EventInstance instance = RuntimeManager.CreateInstance(sound);
            instance.setParameterByNameWithLabel(parameterName, parameterValue);
            instance.start();
            _oneShotInstances.Add(instance);
        }

        public void StopAllTrackedOneShots()
        {
            foreach (EventInstance instance in _oneShotInstances.Where(instance => instance.isValid()))
            {
                instance.stop(0);
                instance.release();
            }

            _oneShotInstances.Clear();
        }

        public void SetGlobalParameter(string parameterName, float value)
        {
            RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
        }

        public void SetGlobalParameter(string parameterName, string label)
        {
            RuntimeManager.StudioSystem.setParameterByNameWithLabel(parameterName, label);
        }

        public void PlayPersistent(EventReference sound, GameObject sourceObject = null,
            Rigidbody sourceRigidbody = null
        )
        {
            if (_activeEvents.ContainsKey(sound))
                return;

            EventInstance instance = RuntimeManager.CreateInstance(sound);

            instance.start();
            _activeEvents[sound] = instance;
        }

        /// <summary>
        ///     Currently for persistent params we have param:
        /// </summary>
        public void PlayPersistent(EventReference sound, string parameterName, string parameterValue,
            GameObject sourceObject = null, Rigidbody sourceRigidbody = null
        )
        {
            if (sound.IsNull || _activeEvents.ContainsKey(sound))
                return;

            EventInstance instance = RuntimeManager.CreateInstance(sound);
            instance.setParameterByNameWithLabel(parameterName, parameterValue);

            instance.start();
            _activeEvents[sound] = instance;
        }

        public void StopPersistent(EventReference sound)
        {
            if (_activeEvents.TryGetValue(sound, out EventInstance instance))
            {
                instance.stop(0);
                instance.release();
                _activeEvents.Remove(sound);
            }
        }

        public void StopAllPersistentSounds()
        {
            foreach (EventInstance instance in _activeEvents.Values)
            {
                instance.stop(0);
                instance.release();
            }

            _activeEvents.Clear();
        }



        #endregion
    }
}