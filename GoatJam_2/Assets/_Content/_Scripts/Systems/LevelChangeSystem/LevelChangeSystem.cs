using UnityEngine;
using UnityEngine.SceneManagement;
using Yaygun.Utilities.Singleton;

namespace Yaygun.Systems.LevelChange
{
    public class LevelChangeSystem : Singleton<LevelChangeSystem>
    {
        [SerializeField]
        private LevelChanger _levelChanger;

        [field:SerializeField] 
        public LevelHolderSO Levels { get; private set; }

        public void ReloadScene()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            LoadScene(currentScene);
        }

        public void LoadGameScene()
        {
            LoadScene(Levels.GetGameScene().LevelName);
        }

        public void LoadMenuScene()
        {
            LoadScene(Levels.GetMenuScene().LevelName);
        }

        public void LoadGameSceneWithIndex(int levelIndex)
        {
            Levels.CurrentLevel = levelIndex;
            LoadGameScene();
        }

        private void LoadScene(string sceneName)
        {
            _ = _levelChanger.ChangeSceneAsync(sceneName);
        }
    }
}