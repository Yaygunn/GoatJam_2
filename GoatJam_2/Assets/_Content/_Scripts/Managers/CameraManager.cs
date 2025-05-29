using Sirenix.OdinInspector;
using UnityEngine;
using Yaygun.Utilities.Singleton;

namespace Yaygun.Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        [field: FoldoutGroup("References"), SerializeField]
        public Camera Camera { get; private set; }
    }
}
