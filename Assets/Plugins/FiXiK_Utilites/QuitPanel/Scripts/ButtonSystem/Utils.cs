using UnityEngine;

namespace FiXiK_Utilites.QuitPanel
{
    public static class Utils
    {
        public static Vector2 GetCursorPositionOnCanvas(Canvas canvas)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out Vector2 cursorPosition
            );

            return cursorPosition;
        }
    }
}