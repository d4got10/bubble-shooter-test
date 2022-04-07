using UnityEngine;

namespace BubbleShooter
{
    public static class MapUtils
    {
        private const float TileHeight = 1.15f;


        public static Vector2 RelativeToWorldPosition(Vector2Int relativePosition)
        {
            Vector2 position = relativePosition;
            if (relativePosition.y % 2 != 0)
                position.x += 0.5f;
            position.y /= TileHeight;

            return position;
        }

        public static Vector2Int WorldToRelativePosition(Vector2 position)
        {
            float x = position.x;

            var relativePosition = new Vector2Int();
            relativePosition.y = Mathf.RoundToInt(position.y * TileHeight);

            if (relativePosition.y % 2 != 0)
                x -= 0.5f;

            relativePosition.x = Mathf.RoundToInt(x);

            return relativePosition;
        }
    }
}