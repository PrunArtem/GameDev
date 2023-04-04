using System;
using UnityEngine;
using Animation;

namespace Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {        
        private Rigidbody2D _rigidBody;

        [SerializeField] private AnimatorController _animatorController;

        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;
        private Vector2 _movement;

        [Header("Jumping")]
        [SerializeField] private float _jumpForce;

        [Header("Attacking")]
        private bool _isAttacking;

        public void Jump()
        {
            Vector2 velocity = _rigidBody.velocity;
            if (velocity.y == 0)
            {
                velocity.y = _jumpForce;
                _rigidBody.velocity = velocity;
            }
        }

        public void StartAttack()
        {
            if (!_animatorController.PlayAnimation(AnimationType.Attack, true))
                return;

            _animatorController.ActionRequested += Attack;
            _animatorController.AnimationEnded += EndAttack;
        }

        private void Attack()
        {
            Debug.Log("Attacked");
        }

        private void EndAttack()
        {
            _animatorController.ActionRequested -= Attack;
            _animatorController.AnimationEnded -= EndAttack;
            _animatorController.PlayAnimation(AnimationType.Attack, false);
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

        private void UpdateAnimations()
        {
            _animatorController.PlayAnimation(AnimationType.Running, _movement.magnitude > 0);
            _animatorController.PlayAnimation(AnimationType.Jump, _rigidBody.velocity.y > 0);
            _animatorController.PlayAnimation(AnimationType.Fall, _rigidBody.velocity.y < 0);
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
