using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Yaygun
{
    public static class SpriteAnim
    {
        private static SpriteHolderSO _spriteHolder;

        public static async UniTask PlayAnim(Image image, int speed = 1)
        {
            if (_spriteHolder == null)
                LoadSpriteHolder();

            if (_spriteHolder == null || _spriteHolder.sprites == null || _spriteHolder.sprites.Length == 0)
            {
                Debug.LogWarning("SpriteHolder is not loaded or empty.");
                return;
            }

            int i = (speed == -1) ? _spriteHolder.sprites.Length - 1 : 0;

            while (i >= 0 && i < _spriteHolder.sprites.Length)
            {
                image.sprite = _spriteHolder.sprites[i];
                await UniTask.WaitForSeconds(1 / 24f);
                i += speed;
            }
        }

        private static void LoadSpriteHolder()
        {
            _spriteHolder = Resources.Load<SpriteHolderSO>("New Sprite Holder");
            if (_spriteHolder == null)
                Debug.LogError("SpriteHolderSO not found in Resources with name 'New Sprite Holder'");
        }
    }
}