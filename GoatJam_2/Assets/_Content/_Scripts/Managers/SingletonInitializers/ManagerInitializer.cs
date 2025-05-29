using UnityEngine;
using Yaygun.Data;
using Yaygun.Utilities.Singleton;

namespace Yaygun.Managers.SingletonInitializers
{
    public class ManagerInitializer : SingletonInitializer<ManagerInitializer>
    {
        [SerializeField] SpawnSingleton<GlobalObjects>  _globalObjects;
        [SerializeField] SpawnSingleton<PauseManager>  _pauseManager;
        [SerializeField] SpawnSingleton<CursorManager>  _cursorManager;

        protected override void Initialize()
        {
            Spawn(_globalObjects);
            Spawn(_pauseManager);
            Spawn(_cursorManager);
        }
    }
}
