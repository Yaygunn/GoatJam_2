using UnityEngine;
using Yaygun.Systems;
using Yaygun.Systems.LevelChange;
using Yaygun.Utilities.Singleton;

namespace Yaygun.Managers.SingletonInitializers
{
    public class PersistentInitializer : SingletonInitializer<PersistentInitializer>
    {
        [SerializeField] SpawnSingleton<LevelChangeSystem> _levelChangeSystem;
        
        [SerializeField] SpawnSingleton<SaveSystem> _saveSystem;
        
        protected override void Initialize()
        {
            DontDestroyOnLoad(gameObject);
            
            Spawn(_levelChangeSystem);
            Spawn(_saveSystem);
        }
    }
}
