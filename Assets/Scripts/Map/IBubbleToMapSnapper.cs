using UnityEngine;

namespace BubbleShooter
{
    public interface IBubbleToMapSnapper
    {
        void SnapToMap(Bubble bubble, Vector2 position);
    }
}