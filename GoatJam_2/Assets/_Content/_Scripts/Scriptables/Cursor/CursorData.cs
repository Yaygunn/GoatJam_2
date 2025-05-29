using Sirenix.OdinInspector;
using UnityEngine;

namespace Yaygun.Scriptables
{
	[CreateAssetMenu(fileName = "SO_CursorData", menuName = "SO/Cursor/Data")]
	public class CursorData : ScriptableObject
	{
		[SerializeField] private Texture2D _cursorTexture;
		
		[SerializeField, OnValueChanged("OnHotSpotChanged")] private Vector2 _hotSpot;

		
		public Texture2D Texture => _cursorTexture;

		public Vector2 HotSpot => _hotSpot;

		private void OnHotSpotChanged()
		{
			_hotSpot.x = Mathf.Clamp(_hotSpot.x, 0, 1f);
			_hotSpot.y = Mathf.Clamp(_hotSpot.y, 0, 1f);
		}
	}
}
