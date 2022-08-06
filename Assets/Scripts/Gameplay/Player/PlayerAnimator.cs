using UnityEngine;

namespace Gameplay
{
    public class PlayerAnimator
    {
        private const float AnimationTransitionTime = 0.25f;
        private readonly int Idle = Animator.StringToHash("Idle");
        private readonly int Run = Animator.StringToHash("Run");
        private AnimationState _activeAnimation;
        private Animator _animator;
        public float DefaultAnimatorSpeed { get; private set; }

        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
            _animator.CrossFade(Idle, AnimationTransitionTime);
            DefaultAnimatorSpeed = _animator.speed;
        }

        public void ChangeAnimation(AnimationState animation)
        {
            if (_activeAnimation == animation)
                return;

            _activeAnimation = animation;
            switch (_activeAnimation)
            {
                case AnimationState.Idle:
                    _animator.CrossFade(Idle, AnimationTransitionTime);
                    break;

                case AnimationState.Run:
                    _animator.CrossFade(Run, AnimationTransitionTime);
                    break;
            }
        }
        public void ChangeAnimatorSpeed(float speed) =>
            _animator.speed = speed;

    }
}