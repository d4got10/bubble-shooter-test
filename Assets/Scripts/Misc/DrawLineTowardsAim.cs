using UnityEngine;

namespace BubbleShooter
{
    public class DrawLineTowardsAim : MonoBehaviour
    {
        [SerializeField] private BubbleShooterAim _aim;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField, Min(0.01f)] private float _length = 1;


        private void Awake()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.position + GetScaledDirection());
        }

        private void Update()
        {
            if (_aim.IsInitialized == false)
                _lineRenderer.SetPosition(1, transform.position);
            else
                _lineRenderer.SetPosition(1, transform.position + GetScaledDirection());
        }

        private Vector3 GetScaledDirection()
        {
            return _length * (Vector3)_aim.Direction;
        }
    }
}