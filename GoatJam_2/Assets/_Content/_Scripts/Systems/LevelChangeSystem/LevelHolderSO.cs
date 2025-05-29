using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        [FoldoutGroup("Game"), SerializeField]
        private SLevelData[] _levels;

        public int NumberOfLevels => _levels.Length;

        public SLevelData GetNextScene() => GetNextScene(SceneManager.GetActiveScene().name);
        
        public int GetCurrentGameSceneIndex()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            for (int i=0; i<_levels.Length; i++)
                if(sceneName == _levels[i].LevelName)
                    return i;
            Debug.LogError("Current scene is not a game scene");
            return -1;
        }
        
        public SLevelData GetGameSceneWithIndex(int index)
        {
            if (_levels.Length > index)
                return _levels[index];
            Debug.LogError("wanted scene is not in levels hierarchy");
            return _endLevel;
        }
        
        private SLevelData GetNextScene(string currentScene)
        {
            for (int i = 0; i < _levels.Length - 1; i++)
                if (_levels[i].LevelName == currentScene)
                    return _levels[i + 1];

            if (_levels[NumberOfLevels - 1].LevelName == currentScene)
                return _endLevel;

            Debug.LogError("Current scene is not in levels hierarchy");
            return _endLevel;
        }

        public SLevelData GetEndScene() => _endLevel;

        public SLevelData GetMenuScene() => _startMenu;

        public SLevelData GetSplashScene() => _splash;
    }
    
    [System.Serializable]
    public struct SLevelData
    {
        [field:SerializeField] public string LevelName { get; private set; }
    }
}