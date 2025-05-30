using UnityEngine;
using Yaygun.Systems;
using Yaygun.Systems.LevelChange;
using Yaygun.Utilities.Singleton;
using YInput;

namespace Yaygun.Managers.SingletonInitializers
{
    public class PersistentInitializer : SingletonInitializer<PersistentInitializer>
    {
        [SerializeField] SpawnSingleton<LevelChangeSystem> _levelChangeSystem;
        
        [SerializeField] SpawnSingleton<SaveSystem> _saveSystem;
        
        [SerializeField] SpawnSingleton<InputHandler> _inputHandler;
        
        protected override void Initialize()
        {
            DontDestroyOnLoad(gameObject);
            
            Spawn(_levelChangeSystem);
            Spawn(_saveSystem);
            Spawn(_inputHandler);
        }
    }
}
