using System;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {        
        private Rigidbody2D _rigidBody;

        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;

        [Header("Jumping")]
        [SerializeField] private float _jumpForce;

        public void Jump()
        {
            Vector2 velocity = _rigidBody.velocity;
            if (velocity.y == 0)
            {
                velocity.y = _jumpForce;
                _rigidBody.velocity = velocity;
            }
        }

        public void MoveHorizontally(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _rigidBody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidBody.velocity = velocity;
        }

        private void SetDirection(float direction) 
        {
            if ((_faceRight && direction < 0) ||
                (!_faceRight && direction > 0))
                Flip();     
        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }
    }

}
