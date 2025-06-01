using FMODUnity;
using UnityEngine;

namespace By2m.Systems
{
    public static class AudioPlay
    {
        private static AudioDataSO _data => AudioSystem.Instance.AudioData;

        public static void Initialize()
        {
        }

        public static void ButtonOn()
        {
            Play(_data.ButtonOn);
        }
        
        public static void ButtonOf()
        {
            Play(_data.ButtonOf);
        }
        
        public static void DoorOpen()
        {
            Play(_data.DoorOpen);
        }
        
        public static void DoorClose()
        {
            Play(_data.DoorClose);
        }

        public static void Slime()
        {
            Play(_data.Slime);
        }

        public static void GraplingGun()
        {
            Play(_data.GraplingGun);
        }

        public static void Death()
        {
            Play(_data.Death);
        }

        public static void LossLegg()
        {
            Play(_data.LegLoss);
        }

        private static void Play(EventReference eventReference)
        {
            AudioSystem.Instance.PlayOneShotAndForget(eventReference);

        }
    }
}