using UnityEngine;

namespace BubbleShooter
{
    public class BubblePainter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;


        public void SetColor(Color color)
        {
            _renderer.color = color;
        }
    }
}