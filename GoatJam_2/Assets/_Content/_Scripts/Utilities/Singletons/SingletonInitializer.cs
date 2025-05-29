using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Yaygun.Utilities.Singleton
{
	public class SingletonInitializer<T> : MonoBehaviour where T : MonoBehaviour
	{
        protected static T Instance { get; private set; }

        protected virtual void Initialize() { }
        protected virtual void Destruction() { }

        protected virtual void OnEnable()
        {
            if (Instance == null)
            {
                Instance = this as T;
                Initialize();
            }
            else if (Instance != this)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Destruction();
        }

        protected void Spawn(SpawnSingleton singleton)
        {
            if (singleton.ShouldSpawn)
                Instantiate(singleton.Prefab, transform);
        }

        protected void Spawn(SpawnSingletonParentClass singleton)
        {
            if(singleton.ShouldSpawn())
                Instantiate(singleton.GetPrefab(), transform);
        }
    }


    [Serializable]
    public class SpawnSingleton
    {
        [HorizontalGroup("SpawnGroup", Width = 20), LabelText(""), SerializeField]
        public bool ShouldSpawn = true;

        [HorizontalGroup("SpawnGroup"), LabelText(""), SerializeField]
        public GameObject Prefab;
    }

    public abstract class SpawnSingletonParentClass
    {
        public abstract bool ShouldSpawn();
        public abstract GameObject GetPrefab();
    }

    [Serializable]
    public class SpawnSingleton<T> : SpawnSingletonParentClass where T : MonoBehaviour
    {
        [HorizontalGroup("SpawnGroup", Width = 20), LabelText(""), SerializeField]
        private bool _shouldSpawn = true;

        [HorizontalGroup("SpawnGroup"), LabelText(""), SerializeField]
        private T Prefab;

        public override GameObject GetPrefab()
        {
            return Prefab.gameObject;
        }

        public override bool ShouldSpawn()
        {
            return _shouldSpawn;
        }
    }
}
