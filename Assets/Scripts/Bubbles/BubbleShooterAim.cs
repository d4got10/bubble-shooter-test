using UnityEngine;

namespace BubbleShooter
{
    public class BubbleShooterAim : MonoBehaviour, IPauseListener
    {
        [SerializeField, Range(0, 89)] private float _halfAngleBoundry = 88f;


        public Vector2 Direction { get; private set; }
        public bool IsInitialized { get; private set; }


        private IInputSystem _inputSystem;
        private Camera _mainCamera;
        private bool _isPaused = false;


        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Init(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            Direction = Vector2.up;
            IsInitialized = true;
        }

        public void Deinit()
        {
            Direction = Vector2.up;
            IsInitialized = false;
        }


        private void Update()
        {
            if (_isPaused || IsInitialized == false) return;

            Direction = GetDirectionTo(_inputSystem.MousePosition); 
        }


        private Vector3 GetDirectionTo(Vector2 touchPosition)
        {
            var worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);

            var projectedPosition = Vector3.ProjectOnPlane(worldPosition, transform.forward);

            var direction = projectedPosition - transform.position;

            direction = ClampVectorBetweenAngles(direction, -_halfAngleBoundry, _halfAngleBoundry);

            return direction.normalized;
        }

        private Vector2 ClampVectorBetweenAngles(Vector2 vector, float minAngle, float maxAngle)
        {
            float angle = -Vector2.SignedAngle(vector, Vector2.up);
            if(angle < minAngle)
            {
                var rotation = Quaternion.Euler(0, 0, minAngle);
                return rotation * Vector2.up;
            }
            else if(angle > maxAngle)
            {
                var rotation = Quaternion.Euler(0, 0, maxAngle);
                return rotation * Vector2.up;
            }

            return vector;
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