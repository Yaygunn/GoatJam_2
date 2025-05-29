using Sirenix.OdinInspector;
using UnityEngine;

namespace Yaygun.Managers
{
    public class PauseManager : MonoBehaviour
    {

        [FoldoutGroup("References"), SerializeField]
        private GameObject pauseMenu;


        private bool _isPaused;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                if (_isPaused)
                    Resume();
                else
                    Pause();
        }

        public void Pause()
        {
            _isPaused = true;
            print("disable input");
            print("show cursor");
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        public void Resume()
        {
            _isPaused = false;
            print("enable input");
            print(" hide cursor");
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}