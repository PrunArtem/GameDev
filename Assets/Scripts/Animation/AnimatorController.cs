using System;
using UnityEngine;

namespace Animation
{
    public class AnimatorController : MonoBehaviour
    {
        private AnimationType _currentAnimationType;

        [SerializeField] private Animator _animator;

        public event Action ActionRequested;
        public event Action AnimationEnded;

        public bool PlayAnimation(AnimationType animationType, bool active)
        {
            if (!active)
            {
                if (_currentAnimationType == AnimationType.Idle || _currentAnimationType != animationType)
                    return false;

                _currentAnimationType = AnimationType.Idle;
                PlayAnimation(_currentAnimationType);
                return false;
            }

            if (_currentAnimationType >= animationType)
                return false;

            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
            return true;
        }

        public void PlayAnimation(AnimationType animationType)
        {
            _animator.SetInteger(nameof(AnimationType), (int)animationType);
        }

        public void Start()
        {
            _animator.GetComponent<Animator>();
        }

        protected void OnActionRequested() => ActionRequested?.Invoke();
        protected void OnAnimationEnded() => AnimationEnded?.Invoke();
    }
}
