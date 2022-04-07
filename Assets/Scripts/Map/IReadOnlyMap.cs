using UnityEngine;

namespace BubbleShooter
{
    public interface IReadOnlyMap
    {
        public event OnMapChanged Changed;
        public Vector2Int Size { get; }
        public BubbleType this[Vector2Int position] { get; }
    }
}