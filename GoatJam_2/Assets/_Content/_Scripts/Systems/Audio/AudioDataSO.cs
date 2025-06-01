using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace By2m.Systems
{
    [CreateAssetMenu(fileName = "AudioDataSO", menuName = "SO/AudioData")]
    public class AudioDataSO : ScriptableObject
    {
        [field: FoldoutGroup("Music"), SerializeField]
        public EventReference GameMusic { get; private set; }
        
        
        [field :FoldoutGroup("SFX")]
        [field: FoldoutGroup("SFX/Pawn"), SerializeField]
        public EventReference ButtonOn { get; private set; }
        [field: FoldoutGroup("SFX/Pawn"), SerializeField]
        public EventReference ButtonOf { get; private set; }
        [field: FoldoutGroup("SFX/Pawn"), SerializeField]
        public EventReference DoorOpen { get; private set; }
        [field: FoldoutGroup("SFX/Pawn"), SerializeField]
        public EventReference DoorClose { get; private set; }
        [field: FoldoutGroup("SFX/Pawn"), SerializeField]
        public EventReference Slime { get; private set; }
        
        
        [field: FoldoutGroup("SFX/Card"), SerializeField]
        public EventReference GraplingGun { get; private set; }
        [field: FoldoutGroup("SFX/Card"), SerializeField]
        public EventReference LegLoss { get; private set; }
        [field: FoldoutGroup("SFX/Card"), SerializeField]
        public EventReference Death { get; private set; }
    }
}
