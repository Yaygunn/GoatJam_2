using UnityEngine;

namespace Yaygun
{
    [CreateAssetMenu(fileName = "New Sprite Holder", menuName = "SO/Sprite Holder")]
    public class SpriteHolderSO : ScriptableObject
    {
        public Sprite[] sprites;
    }
}