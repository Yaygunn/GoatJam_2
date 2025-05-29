using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Yaygun.UI.Menu
{
    public class LevelSelectionElement : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField]
        private TextMeshProUGUI _levelIndexText;
        [FoldoutGroup("References"), SerializeField]
        private Image _image;

        [FoldoutGroup("Sprite"), SerializeField]
        private Sprite _unlockedTexture;

        [FoldoutGroup("Sprite"), SerializeField]
        private Sprite _lockedTexture;

        [FoldoutGroup("Sprite"), SerializeField]
        private float _lockedAlpha = 0.7f;

        private SLevelInfo _levelInfo;

        private Action<int> _onLevelSelected;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void InitializeElement(SLevelInfo levelInfo, Action<int> onLevelSelected)
        {
            _levelInfo = levelInfo;
            _levelIndexText.text = (levelInfo.LevelIndex + 1).ToString();
            _onLevelSelected = onLevelSelected;

            if (_levelInfo.Islocked)
                InitLocked();
            else
                InitUnlocked();
        }

        private void OnClick()
        {
            if (!_levelInfo.Islocked)
                _onLevelSelected?.Invoke(_levelInfo.LevelIndex);
        }

        private void InitUnlocked()
        {
            _image.sprite = _unlockedTexture;
        }

        private void InitLocked()
        {
            _image.sprite = _lockedTexture;
            Color c = _image.color;
            c.a = _lockedAlpha;
            _image.color = c;

            if (_image.TryGetComponent(out ButtonAnimation buttonAnimation))
                Destroy(buttonAnimation);
        }
    }
}