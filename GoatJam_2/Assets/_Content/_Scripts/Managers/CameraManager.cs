using Sirenix.OdinInspector;
using Unity.Cinemachine;
using UnityEngine;
using Yaygun.Utilities.Singleton;

namespace Yaygun.Managers
{
    public class CameraManager : Singleton<CameraManager>
    {
        [field: FoldoutGroup("References"), SerializeField]
        public Camera Camera { get; private set; }
        [field: FoldoutGroup("References"), SerializeField]
        public CinemachineCamera VirtualCamera { get; private set; }

        public void FollowMe(Transform target)
        {
            VirtualCamera.Follow = target;
        }
    }
}
