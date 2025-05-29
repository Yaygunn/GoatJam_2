using UnityEngine;
using Yaygun.Utilities.Singleton;

namespace Yaygun.Systems
{
    public class SaveSystem : Singleton<SaveSystem>
    {
        public static LevelSave LevelSave { get; private set; }
        
        public static VolumeSettings VolumeSettings { get; private set; }
        
        protected override void Initialize()
        {
            LevelSave = new LevelSave();
            VolumeSettings = new VolumeSettings();
        }

        public void SaveBoolPref(string boolName, bool isTrue)
        {
            PlayerPrefs.SetInt(boolName, isTrue ? 1 : 0);
        }
        
        public bool LoadBoolPref(string boolName)
        {
            return PlayerPrefs.GetInt(boolName) == 1;
        }
    }
}
