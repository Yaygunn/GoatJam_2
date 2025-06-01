using Sirenix.OdinInspector;
using UnityEngine;
using System;
using UnityEngine.Serialization;
using Yaygun.Managers;

namespace Yaygun
{
    public class LevelController : MonoBehaviour
    {
        public Door Door { get; private set; }

        public event Action Ev_EnteredLevelArea;
        
        [field:SerializeField] public SpawnPoint SpawnPoint { get; private set; }


        public int LevelVersion { get; set; }
        public void Initialize(int levelVersion)
        {
            LevelVersion = levelVersion;
            Door = TryGetLevelDoor();
        }

        public void OpenNextRoomDoor()
        {
            if(!Door)
                print("door is null");
            Door?.OpenDoor();
            LevelManager.Instance.LoadNextLevel();
        }

        public void EnteredLevelArea()
        {
            LevelManager.Instance.AchieveSaveLevel(this);
            Ev_EnteredLevelArea?.Invoke();
        }

        public void CloseNextRoomDoor()
        {
            Door?.CloseDoor();
            LevelManager.Instance.DeleteNextLevel(this);
        }

        

        private Door TryGetLevelDoor()
        {
            return GetComponentInChildren<Door>();
        }
        
    }
}
