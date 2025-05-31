using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Yaygun.Systems.LevelChange
{
    [CreateAssetMenu(menuName = "SO/Systems/LevelChange")]
    public class LevelHolderSO : ScriptableObject
    {
        [FoldoutGroup("NonGame"), SerializeField]
        private SLevelData _splash;
        [FoldoutGroup("NonGame"), SerializeField]
        private SLevelData _startMenu;
        [FoldoutGroup("NonGame"), SerializeField]
        private SLevelData _endLevel;
        [FoldoutGroup("NonGame"), SerializeField]
        private SLevelData _gameLevel;

        [FoldoutGroup("Game"), SerializeField]
        private SLevelPrefab[] _levels;

        public int NumberOfLevels => _levels.Length;

        public int CurrentLevel;

        public SLevelPrefab GetGameSceneWithIndex(int index)
        {
            if (_levels.Length > index)
                return _levels[index];
            Debug.LogError("wanted scene is not in levels hierarchy");
            return _levels[0];
        }
        
        public SLevelData GetEndScene() => _endLevel;

        public SLevelData GetMenuScene() => _startMenu;

        public SLevelData GetSplashScene() => _splash;
        public SLevelData GetGameScene() => _gameLevel;
    }
    
    [System.Serializable]
    public struct SLevelData
    {
        [field:SerializeField] public string LevelName { get; private set; }
    }
    
    [System.Serializable]
    public struct SLevelPrefab
    {
        [field:SerializeField] public GameObject LevelPrefab { get;  private set; }

        [field: SerializeField] public int LevelVersionIndex { get; private set; }

        [field: SerializeField] public bool HasLegs { get; private set; }
    }
}