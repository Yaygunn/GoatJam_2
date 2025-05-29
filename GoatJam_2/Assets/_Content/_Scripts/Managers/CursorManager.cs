using UnityEngine;
using Yaygun.Utilities.Singleton;
using Yaygun.Scriptables;

namespace Yaygun.Managers
{
	public class CursorManager : Singleton<CursorManager>
	{
		[SerializeField] private CursorHolder _cursors;

		protected override void Initialize()
		{
			SetCursor(_cursors.MenuCursor);
		}


		private void SetCursor(CursorData cursorData)
		{
            
			Cursor.SetCursor(cursorData.Texture, GetHotSpot(cursorData), CursorMode.Auto);
		}

		private Vector2 GetHotSpot(CursorData cursorData)
		{
			Vector2 hotSpot;
			hotSpot.x = cursorData.Texture.width * cursorData.HotSpot.x;
			hotSpot.y = cursorData.Texture.height * cursorData.HotSpot.y;
			return hotSpot;
		}
	}
}
