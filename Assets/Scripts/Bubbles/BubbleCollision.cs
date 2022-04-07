using UnityEngine;

namespace BubbleShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BubbleCollision : MonoBehaviour
    {
        public event System.Action<Collision2D> Collided;


        private CircleCollider2D _collider;


        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collided?.Invoke(collision);
        }


        public void Enable()
        {
            _collider.enabled = true;
        }

        public void Disable()
        {
            _collider.enabled = false;
        }
    }
}