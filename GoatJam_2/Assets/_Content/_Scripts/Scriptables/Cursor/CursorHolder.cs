using UnityEngine;

namespace Yaygun.Scriptables
{
    [CreateAssetMenu(fileName = "SO_CursorHolder", menuName = "SO/Cursor/Holder")]
    public class CursorHolder : ScriptableObject
    {
        public CursorData GameplayCursor;
            
        public CursorData MenuCursor;

    }
}   