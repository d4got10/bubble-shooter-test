using UnityEngine;

namespace BubbleShooter
{

    public class Level : MonoBehaviour
    {
        public event System.Action Completed;
        public event System.Action Failed;


        [SerializeField] private MapInWorld _mapInWorld;
        [SerializeField] private BubbleShooter _shooter;
        [SerializeField] private BubbleMovementSystem _movementSystem;
        [SerializeField] private BubbleFactory _bubbleFactory;


        public bool IsInitialized { get; private set; }


        public void Init(Map map)
        {
            _mapInWorld.Init(map, _bubbleFactory);
            _movementSystem.Init(_mapInWorld);
            _shooter.Init(_bubbleFactory, map, _movementSystem, InputSystemProvider.Current);

            _mapInWorld.ClearedAll += OnClearedAll;
            _mapInWorld.ReachedFloor += OnReachedFloor;

            IsInitialized = true;
        }

        public void Deinit()
        {
            _mapInWorld.ClearedAll -= OnClearedAll;

            _shooter.Deinit();
            _movementSystem.Deinit();
            _mapInWorld.Deinit();

            IsInitialized = false;
        }


        private void OnClearedAll()
        {
            Completed?.Invoke();
        }

        private void OnReachedFloor()
        {
            Failed?.Invoke();
        }
    }
}