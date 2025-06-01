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

        public void DeleteNextLevel(LevelController level)
        {
            for (int i = 0; i < _levelHolder.NumberOfLevels; i++)
                if (_loadedLevels[i].LevelController == level)
                {
                    if(_loadedLevels.Count > i +1)
                        DestroyLane(i+1);
                }
        }

        public void AchieveSaveLevel(LevelController level)
        {
            SLevelManage levelManage = _loadedLevels[0];
            foreach (var VARIABLE in _loadedLevels)
                if(VARIABLE.LevelController == level)
                    levelManage = VARIABLE;
            _levelHolder.CurrentLevel = levelManage.LevelIndex;
            SaveSystem.LevelSave.UnlockLevel(levelManage.LevelIndex);
            RemovePreviousLevel();
        }
        
        private void LoadNewLevel(SLevelPrefab levelPrefab, int levelIndex)
        {
            LevelController level = Instantiate(levelPrefab.LevelPrefab).GetComponent<LevelController>();
            _loadedLevels.Add(new SLevelManage(levelPrefab, level, levelIndex));
            level.Initialize(levelPrefab.LevelVersionIndex);
        }

        public async void RemovePreviousLevel()
        {
            if (_loadedLevels.Count < 3)
                return;
            LevelController level = _loadedLevels[0].LevelController;
            _loadedLevels.RemoveAt(0);

            Door door = level.Door;
            if (door)
            {
                door.CloseDoor();
                door.transform.SetParent(_loadedLevels[0].LevelController.transform);
                await UniTask.WaitForSeconds(Door.AnimTime);
            }
            
            Destroy(level.gameObject);
        }
        
        public void DeleteAllLoadedAndLoadNextLevel()
        {
            int levelIndex = _loadedLevels[_loadedLevels.Count - 1].LevelIndex;
            foreach (var VARIABLE in _loadedLevels)
                Destroy(VARIABLE.LevelController.gameObject);
            
            _loadedLevels.Clear();
            OpenFromZeroLevel(levelIndex +1, false);
        }

        public void DestroyLane(int laneIndexInLoaded)
        {
            Destroy(_loadedLevels[laneIndexInLoaded].LevelController.gameObject);
            _loadedLevels.RemoveAt(laneIndexInLoaded);
        }

        private void OpenFromZeroLevel(int  levelIndex, bool withProviousDoor = true)
        {
            if(levelIndex >0)
                if(withProviousDoor)
                    LoadNewLevel(_levelHolder.GetGameSceneWithIndex(levelIndex -1), levelIndex -1);
                else
                    print("spawn level without door");
            LoadNewLevel(_levelHolder.GetGameSceneWithIndex(levelIndex ), levelIndex );

            if (_loadedLevels.Count > 1)
            {
                
                _loadedLevels[0].LevelController.Initialize(_loadedLevels[0].LevelPrefab.LevelVersionIndex);
            
                Door door = _loadedLevels[0].LevelController.Door;
                if(door)
                    door.transform.SetParent(_loadedLevels[1].LevelController.transform);
                else 
                    print("previous level has no door.");
            
                Destroy(_loadedLevels[0].LevelController.gameObject);
                _loadedLevels.RemoveAt(0);
            }
            
            SLevelPrefab levelPrefab = _loadedLevels[0].LevelPrefab;
            LevelController levelController = _loadedLevels[0].LevelController;
            
            SLevelManage levelManage = _loadedLevels[0];
            levelManage.LevelController.Initialize(levelManage.LevelPrefab.LevelVersionIndex);
            
            Vector3 spawnPos = levelManage.LevelController.SpawnPoint.transform.position;

            spawnPos.z = 0;
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