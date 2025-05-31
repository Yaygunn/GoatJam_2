using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace Yaygun.Systems.LevelChange
{
    public class LevelChanger : MonoBehaviour
    {
        [FoldoutGroup("UI Elements"), SerializeField]
        private GameObject _canvas;
        [FoldoutGroup("UI Elements"), SerializeField]
        private Transform _loadingScreen;
        [FoldoutGroup("UI Elements"), SerializeField]
        private Image _loadingBar;

        [FoldoutGroup("Animation"), SerializeField]
        private float _startX = -1920;
        [FoldoutGroup("Animation"), SerializeField]
        private float _endX = 1920;
        [FoldoutGroup("Animation"), SerializeField]
        private float _enterTime = 0.5f;
        [FoldoutGroup("Animation"), SerializeField]
        private float _exitTime = 0.5f;

        [FoldoutGroup("Delay Times"), SerializeField] 
        private float _hideDelay = 0.1f;

        private bool _isChangingScene;

        public async UniTask ChangeSceneAsync(string sceneName)
        {
            if (_isChangingScene)
            {
                Debug.LogError($"Scene \"{sceneName}\" is already changing scene");
                return;
            }

            _isChangingScene = true;
            
            ActivateLoadingScreen();
            await SpriteAnim.PlayAnim(_loadingBar,1);
            await UniTask.WaitForSeconds(_enterTime);
            
            await SceneLoadOperation(sceneName);

            await UniTask.WaitForSeconds(_hideDelay);
            SpriteAnim.PlayAnim(_loadingBar, -1).Forget();
            
            _isChangingScene = false;
        }

        private async UniTask SceneLoadOperation(string sceneName)
        {
            AsyncOperation sceneLoadOp = SceneManager.LoadSceneAsync(sceneName);
            if (sceneLoadOp == null)
            {
                Debug.LogError($"[LevelChanger] Failed to load scene: {sceneName}");
                return;
            }

            sceneLoadOp.allowSceneActivation = false;

            while (sceneLoadOp.progress < 0.9f)
            {
                SetLoadingBarValue(sceneLoadOp.progress);
                await UniTask.WaitForSeconds(0.01f);
            }

            SetLoadingBarValue(1f);
            sceneLoadOp.allowSceneActivation = true;
        }

        private void ActivateLoadingScreen()
        {
            if (_canvas != null)
                _canvas.SetActive(true);
            if (_loadingBar != null)
                _loadingBar.fillAmount = 0f;
            if (_loadingScreen == null)
                return;

            /*_loadingScreen.DOKill();
            _loadingScreen.localPosition = new Vector3(_startX, 0f, 0f);
            _loadingScreen.DOLocalMove(Vector3.zero, _enterTime).SetEase(Ease.OutQuint);*/
        }

        private void HideLoadingScreen()
        {
            if (_loadingScreen == null || _canvas == null)
                return;

            _loadingScreen.DOKill();
            _loadingScreen.DOLocalMove(new Vector3(_endX, 0, 0), _exitTime).SetEase(Ease.InQuint)
                          .OnComplete(() => _canvas.SetActive(false));
        }

        private void SetLoadingBarValue(float progress)
        {
            if (_loadingBar == null)
                return;

            _loadingBar.fillAmount = Mathf.Clamp01(progress / 0.9f);
        }
    }
}