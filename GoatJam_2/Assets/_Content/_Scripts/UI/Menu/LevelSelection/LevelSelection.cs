using Sirenix.OdinInspector;
using UnityEngine;
using Yaygun.Systems;
using Yaygun.Systems.LevelChange;

namespace Yaygun.UI.Menu
{
    public class LevelSelectionPanel : MonoBehaviour
    {
        [FoldoutGroup("References"), FoldoutGroup("References/Scene"), SerializeField]
        private Transform _elementContainer;

        [FoldoutGroup("References/Prefab"), SerializeField]
        private LevelSelectionElement _levelSelectionElementPrefab;


        private void Start()
        {
            InitializeSelectionElements();
        }

        private void InitializeSelectionElements()
        {
            int levelCount = LevelChangeSystem.Instance.Levels.NumberOfLevels;

            for (int i = 0; i < levelCount; i++)
                InitializeElement(i);
        }

        private void InitializeElement(int levelIndex)
        {
            LevelSelectionElement element = Instantiate(_levelSelectionElementPrefab, _elementContainer);

            element.InitializeElement(GetLevelInfo(levelIndex), OnLevelSelected);
        }

        private SLevelInfo GetLevelInfo(int levelIndex)
        {
            SLevelInfo levelInfo = new();
            levelInfo.LevelIndex = levelIndex;
            levelInfo.Islocked = !SaveSystem.LevelSave.IsLevelUnlocked(levelIndex);
            return levelInfo;
        }

        private void OnLevelSelected(int levelIndex)
        {
             LevelChangeSystem.Instance.LoadGameSceneWithIndex(levelIndex);
        }
    }

    public struct SLevelInfo
    {
        public int LevelIndex;
        public bool Islocked;
    }
}