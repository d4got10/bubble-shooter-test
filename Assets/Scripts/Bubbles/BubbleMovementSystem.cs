using UnityEngine;

namespace BubbleShooter
{
    public class BubbleMovementSystem : MonoBehaviour, IPauseListener
    {
        public event System.Action Completed;


        [SerializeField, Min(0.01f)] private float _speed = 1f;


        private Vector2 _velocity;
        private Movable _target;
        private Bubble _bubble;
        private IBubbleToMapSnapper _snapper;
        private bool _isPaused;


        public void Init(IBubbleToMapSnapper snapper)
        {
            _snapper = snapper;
        }

        public void Deinit()
        {
            if (_bubble != null)
                Destroy(_bubble.gameObject);
        }


        public void RegisterBubble(Bubble bubble, Vector2 initialPosition, Vector2 initialVelocity)
        {
            _bubble = bubble;

            _target = bubble.gameObject.AddComponent<Movable>();
            _target.SetPosition(initialPosition);
            _velocity = initialVelocity;

            _bubble.Collision.Collided += OnBubbleCollided;
        }

        public void UnregisterBubble()
        {
            Destroy(_target.gameObject);

            _target = null;

            Completed?.Invoke();
        }


        private void Update()
        {
            if (_target == null || _isPaused) return;

            _target.Move(_speed * _velocity);
        }


        private void OnBubbleCollided(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Bubble>(out var bubble))
            {
                SnapBubbleToMap();
                return;
            }

            if (collision.gameObject.TryGetComponent<Wall>(out var wall))
            {
                _velocity.x *= -1;
                return;
            }

            if(collision.gameObject.TryGetComponent<Ceiling>(out var ceiling))
            {
                SnapBubbleToMap();
                return;
            }
        }

        private void SnapBubbleToMap()
        {
            _bubble.Collision.Collided -= OnBubbleCollided;
            _snapper.SnapToMap(_bubble, _bubble.transform.position);
            UnregisterBubble();
        }

        void IPauseListener.Pause()
        {
            _isPaused = true;
        }

        void IPauseListener.Unpause()
        {
            _isPaused = false;
        }
    }
}