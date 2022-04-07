using UnityEngine;

namespace BubbleShooter
{
    public class BubbleShooter : MonoBehaviour
    {
        [SerializeField] private BubbleShooterAim _aim;
        [SerializeField] private BubbleShooterReloader _reloader;


        private BubbleMovementSystem _movementSystem;
        private IInputSystem _inputSystem;


        private bool _canShoot;
        private Bubble _loadedBubble;


        public void Init(IBubbleFactory bubbleFactory, IReadOnlyMap map, BubbleMovementSystem movementSystem, IInputSystem inputSystem)
        {
            _movementSystem = movementSystem;
            _inputSystem = inputSystem;

            _movementSystem.Completed += OnMovementCompleted;
            _inputSystem.Clicked += OnInputSystemClicked;

            _reloader.Init(bubbleFactory, map);
            _aim.Init(inputSystem);

            _canShoot = true;
            Reload();
        }


        public void Deinit()
        {
            if (_loadedBubble != null)
                Destroy(_loadedBubble.gameObject);

            _aim.Deinit();
            _inputSystem.Clicked -= OnInputSystemClicked;
            _movementSystem.Completed -= OnMovementCompleted;
        }


        private void OnInputSystemClicked()
        {
            if (_canShoot == false) return;

            Shoot(transform.position, _aim.Direction);
        }


        private void Shoot(Vector2 position, Vector2 direction)
        {
            _loadedBubble.Collision.Enable();
            _movementSystem.RegisterBubble(_loadedBubble, position, direction);
            _loadedBubble = null;

            _canShoot = false;
        }

        private void Reload()
        {
            if(_loadedBubble != null)
                Destroy(_loadedBubble.gameObject);

            _loadedBubble = _reloader.GetBubble();
            _loadedBubble.Collision.Disable();
            _loadedBubble.transform.position = transform.position;
        }

        private void OnMovementCompleted()
        {
            _canShoot = true;
            Reload();
        }
    }
}