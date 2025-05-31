using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Yaygun.Systems;
using Yaygun.Systems.LevelChange;
using Yaygun.Utilities.Singleton;

namespace Yaygun.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        private static int _currentLevel = 0;
        
        [SerializeField] LevelHolderSO _levelHolder;

        [FoldoutGroup("Player Prefabs"), SerializeField]
        private GameObject _leglessCharacter;
        
        [FoldoutGroup("Player Prefabs"), SerializeField]
        private GameObject _walkingCharacter;
        
        
        private List<SLevelManage> _loadedLevels = new ();

        protected override void Initialize()
        {
            OpenFromZeroLevel(_levelHolder.CurrentLevel);
        }

        public void LoadNextLevel()
        {
            int nextLevelIndex = _levelHolder.CurrentLevel + 1;

            if (_levelHolder.NumberOfLevels <= nextLevelIndex)
            {
                Debug.LogError("Big Error");
                return;
            }
            
            LoadNewLevel(_levelHolder.GetGameSceneWithIndex(nextLevelIndex), nextLevelIndex);
        }

        public void AchieveSaveLevel(LevelController level)
        {
            SLevelManage levelManage = _loadedLevels[0];
            foreach (var VARIABLE in _loadedLevels)
                if(VARIABLE.LevelController == level)
                    levelManage = VARIABLE;
            _levelHolder.CurrentLevel = levelManage.LevelIndex;
            SaveSystem.LevelSave.UnlockLevel(levelManage.LevelIndex);
        }
        
        private void LoadNewLevel(SLevelPrefab levelPrefab, int levelIndex)
        {
            LevelController level = Instantiate(levelPrefab.LevelPrefab).GetComponent<LevelController>();
            _loadedLevels.Add(new SLevelManage(levelPrefab, level, levelIndex));
            level.Initialize(levelPrefab.LevelVersionIndex);
        }

        public async void RemovePreviousLevel()
        {
            LevelController level = _loadedLevels[0].LevelController;
            _loadedLevels.RemoveAt(0);
            
            Door door = level.GetComponent<Door>();
            if (door)
            {
                door.CloseDoor();
                door.transform.SetParent(_loadedLevels[0].LevelController.transform);
                await UniTask.WaitForSeconds(Door.AnimTime);
            }
            
            Destroy(level.gameObject);
        }

        private void OpenFromZeroLevel(int  levelIndex)
        {
            if(levelIndex >0)
                LoadNewLevel(_levelHolder.GetGameSceneWithIndex(levelIndex -1), levelIndex -1);
            LoadNewLevel(_levelHolder.GetGameSceneWithIndex(levelIndex ), levelIndex );

            if (_loadedLevels.Count > 1)
            {
                
                _loadedLevels[0].LevelController.Initialize(_loadedLevels[0].LevelPrefab.LevelVersionIndex);
            
                Door door = _loadedLevels[0].LevelController.Door;
                if(door)
                    door.transform.SetParent(_loadedLevels[1].LevelController.transform);
            
                Destroy(_loadedLevels[0].LevelController.gameObject);
                _loadedLevels.RemoveAt(0);
            }
            
            SLevelPrefab levelPrefab = _loadedLevels[0].LevelPrefab;
            LevelController levelController = _loadedLevels[0].LevelController;
            
            SLevelManage levelManage = _loadedLevels[0];
            levelManage.LevelController.Initialize(levelManage.LevelPrefab.LevelVersionIndex);
            
            Vector3 spawnPos = levelManage.LevelController.SpawnPointIndexes[levelManage.LevelPrefab.LevelVersionIndex].position;

            if(levelManage.LevelPrefab.HasLegs)
                Instantiate(_walkingCharacter, spawnPos, Quaternion.identity);
            else
                Instantiate(_leglessCharacter, spawnPos, Quaternion.identity);
        }
    }

    public struct SLevelManage
    {
        public SLevelPrefab LevelPrefab { get; private set; }
        public LevelController LevelController { get; private set; }
        public int LevelIndex { get; private set; }

        public SLevelManage(SLevelPrefab levelPrefab, LevelController levelController, int levelIndex)
        {
            LevelPrefab = levelPrefab;
            LevelController = levelController;
            LevelIndex = levelIndex;
        }
    }
}