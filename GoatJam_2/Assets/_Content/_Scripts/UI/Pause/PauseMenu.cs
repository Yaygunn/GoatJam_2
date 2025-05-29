using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Yaygun.Managers;
using Yaygun.Systems.LevelChange;

namespace Yaygun.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField]
        private PauseManager _pauseManager;
        [FoldoutGroup("References"), SerializeField]
        private SPanelOpener[] _panelOpeners;
        [FoldoutGroup("References"), SerializeField]
        private GameObject _menuPanel;
        [FoldoutGroup("References"), SerializeField]
        private Button _playButton;
        [FoldoutGroup("References"), SerializeField]
        private Button _restartButton;
        [FoldoutGroup("References"), SerializeField]
        private Button _mainMenuButton;

        private readonly List<GameObject> _panels = new();


        private void OnEnable()
        {
            foreach (SPanelOpener panelOpener in _panelOpeners)
            {
                _panels.Add(panelOpener.Panel);
                GameObject panel = panelOpener.Panel;
                panelOpener.Button.onClick.AddListener(() => OpenPanel(panel));
            }

            _panels.Add(_menuPanel);

            _playButton.onClick.AddListener(() => _pauseManager.Resume());
            _restartButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                LevelChangeSystem.Instance.ReloadScene();
            });
            _mainMenuButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                LevelChangeSystem.Instance.LoadMenuScene();
            });

            OpenPanel(_menuPanel);
        }

        private void OpenPanel(GameObject panel)
        {
            foreach (GameObject go in _panels)
                go.SetActive(false);

            panel.SetActive(true);
        }
    }
}