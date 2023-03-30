using System;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {        
        private Rigidbody2D _rigidBody;
        [SerializeField] private Animator _animator;

        private AnimationType _currentAnimationType;

        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;
        private Vector2 _movement;

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
            _movement.x = direction;
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

        private void PlayAnimation(AnimationType animationType, bool active)
        {
            if (!active)
            {
                if (_currentAnimationType == AnimationType.Idle || _currentAnimationType != animationType)
                    return;

                _currentAnimationType = AnimationType.Idle;
                PlayAnimation(_currentAnimationType);
                return;
            }

            if (_currentAnimationType >= animationType)
                return;

            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
        }

        private void PlayAnimation(AnimationType animationType)
        {
            _animator.SetInteger(nameof(AnimationType), (int)animationType);
        }

        private void UpdateAnimations()
        {
            PlayAnimation(AnimationType.Running, _movement.magnitude > 0);
            PlayAnimation(AnimationType.Jump, _rigidBody.velocity.y > 0);
            PlayAnimation(AnimationType.Fall, _rigidBody.velocity.y < 0);
        }

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            UpdateAnimations();
        }
    }

}
