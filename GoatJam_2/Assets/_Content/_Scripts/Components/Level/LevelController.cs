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
        
        [field:FoldoutGroup("spawn point Referances"), SerializeField]
        public Transform[] SpawnPointIndexes { get; private set; }

        [FoldoutGroup("Level Version Referances"), SerializeField]
        private SLevelObjects[] _levelVersionObjects;

        public int LevelVersion;
        public void Initialize(int levelVersion)
        {
            LevelVersion = levelVersion;
            DestroyOtherVersionObjects();
            Door = TryGetLevelDoor();
        }

        public void OpenNextRoomDoor()
        {
            Door?.OpenDoor();
            LevelManager.Instance.LoadNextLevel();
        }

        public void EnteredLevelArea()
        {
            LevelManager.Instance.AchieveSaveLevel(this);
            Ev_EnteredLevelArea?.Invoke();
        }


        private Door TryGetLevelDoor()
        {
            return GetComponentInChildren<Door>();
        }

        private void DestroyOtherVersionObjects()
        {
            for (int i = 0; i < _levelVersionObjects.Length; i++)
                if(i != LevelVersion)
                    foreach (var VARIABLE in _levelVersionObjects[i].Objects)
                        Destroy(VARIABLE);
        }
    }

    [System.Serializable]
    public struct SLevelObjects
    {
        public GameObject[] Objects;
    }
}
