using UnityEngine;

namespace BubbleShooter
{
    public interface IBubbleFactory
    {
        Bubble Create(BubbleType type);
    }
}