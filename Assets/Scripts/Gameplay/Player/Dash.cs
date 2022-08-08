using UnityEngine;
using Mirror;

namespace Gameplay
{
    public class Dash : NetworkBehaviour
    {
        private const float AnimatorDashSpeed = 4;
        [SerializeField] private float _distance = 10;
        private float DashTime = 0.5f;
        private float _speed;

        [SyncVar(hook = nameof(OnIsDashingChanged))]
        private bool _isDashing;
        private Vector3 _targetPosition;
        private PlayerAnimator _animator;
        public bool IsDashing => _isDashing;


        public void Initialize(PlayerAnimator animator)
        {
            _animator = animator;
            _speed = _distance / DashTime;
        }

        [Client]
        private void Update()
        {
            if (_isDashing)
                Dashing();
        }

        [TargetRpc]
        public void ResetDash() =>
            EndDash();

        [Command]
        public void StartDash()
        {
            if (_isDashing)
                return;
            _isDashing = true;
        }

        [Client]
        private void OnIsDashingChanged(bool oldValue, bool newValue)
        {
            if (newValue == true)
            {
                _targetPosition = transform.position + transform.forward * _distance;
                _animator.ChangeAnimation(AnimationState.Run);
                _animator.ChangeAnimatorSpeed(AnimatorDashSpeed);
            }
            else
                _animator.ChangeAnimatorSpeed(_animator.DefaultAnimatorSpeed);
        }

        [Client]
        private void Dashing()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            if (transform.position == _targetPosition)
                EndDash();
        }

        [Command]
        private void EndDash() =>
            _isDashing = false;

    }
}