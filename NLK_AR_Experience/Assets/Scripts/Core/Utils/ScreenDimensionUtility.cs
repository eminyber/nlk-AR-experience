using NLKARExperience.Core.Models;

using UnityEngine;

namespace NLKARExperience.Core.Utils
{
    public static class ScreenDimensionUtility
    {
        public static Vector2 GetOffset(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new Vector2(-Screen.width, 0);
                case Direction.Right:
                    return new Vector2(Screen.width, 0);
                case Direction.Top:
                    return new Vector2(0, Screen.height);
                case Direction.Bottom:
                    return new Vector2(0, -Screen.height);
                default:
                    return Vector2.zero;
            }
        }

        public static Vector2 GetSize()
        {
            return new Vector2(Screen.width, Screen.height);
        }

        public static float GetAspectRatio()
        {
            return (float)Screen.width / Screen.height;
        }
    }
}