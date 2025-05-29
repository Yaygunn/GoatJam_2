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

        public void LoadFirstGameScene()
        {
            LoadScene(Levels.GetGameSceneWithIndex(0).LevelName);
        }

        public void LoadMenuScene()
        {
            LoadScene(Levels.GetMenuScene().LevelName);
        }

        public void LoadNextScene()
        {
            LoadScene(Levels.GetNextScene().LevelName);
        }

        public void LeadGameSceneWithIndex(int levelIndex)
        {
            LoadScene(Levels.GetGameSceneWithIndex(levelIndex).LevelName);
        }

        private void LoadScene(string sceneName)
        {
            _ = _levelChanger.ChangeSceneAsync(sceneName);
        }
    }
}