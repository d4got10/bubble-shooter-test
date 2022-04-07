using System;
using UnityEngine;

namespace BubbleShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movable : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;


        private Vector2 _velocity;


        private void Awake()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody2D>();
            }

            _rigidbody.gravityScale = 0;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime);
            _rigidbody.velocity = _velocity;
        }


        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void Move(Vector2 velocity)
        {
            _velocity = velocity;
        }
    }
}