using UnityEngine;

namespace BubbleShooter
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField] private BubbleCollision _collision;
        [SerializeField] private BubblePainter _painter;


        public BubbleCollision Collision => _collision;
        public BubblePainter Painter => _painter;
        public BubbleType Type { get; private set; }


        public void Init(BubbleType type)
        {
            Type = type;
        }
    }
}