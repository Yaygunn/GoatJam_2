using UnityEngine;
using Yaygun.Systems.LevelChange;

namespace Yaygun.Systems
{
    public class LevelSave
    {
        #region Const Strings
        private const string _levelIndexString = "LevelMemoryIndex";
        private const string _unlockedString = "Unlocked";
        #endregion

        private SLevelSaveData[] _levelsData;
        
        public LevelSave()
        {
            LoadLevelsData();
        }

        public SLevelSaveData GetLevelSaveData(int levelIndex)
        {
            return _levelsData[levelIndex];
        }

        public bool IsLevelUnlocked(int levelIndex)
        {
            return _levelsData[levelIndex].IsUnlocked;
        }

        public void UnlockNextLevel()
        {
            int nextLevelIndex = LevelChangeSystem.Instance.Levels.GetCurrentGameSceneIndex() + 1;
            if (nextLevelIndex >= _levelsData.Length)
            {
                Debug.LogError($"trying to save level {nextLevelIndex} that is not in range");
                return;
            }

            PlayerPrefs.SetInt(_levelIndexString + nextLevelIndex + _unlockedString, 1);
            _levelsData[nextLevelIndex].IsUnlocked = true;
        }

        private void LoadLevelsData()
        {
            _levelsData = new SLevelSaveData[LevelChangeSystem.Instance.Levels.NumberOfLevels];

            for (int i = 0; i < _levelsData.Length; i++)
            {
                _levelsData[i] = new SLevelSaveData();
                _levelsData[i].IsUnlocked = PlayerPrefs.GetInt(_levelIndexString + i + _unlockedString) == 1;
            }
            
            _levelsData[0].IsUnlocked = true;
        }
    }
    
    public struct SLevelSaveData
    {
        public bool IsUnlocked;
    }
}