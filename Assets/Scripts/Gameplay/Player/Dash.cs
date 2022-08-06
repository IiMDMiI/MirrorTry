using UnityEngine;
using Mirror;

namespace Gameplay
{
    public class Dash : NetworkBehaviour
    {
        [SerializeField] private float _distance = 10;
        private float DashTime = 0.5f;
        private float _speed;

        [SyncVar]
        private bool _isDashing;
        private Vector3 _targetPosition;
        private PlayerAnimator _animator;
        public bool IsDashing => _isDashing;

        public void Initialize(PlayerAnimator animator)
        {
            _animator = animator;
            _speed = _distance / DashTime;
        }

        [ServerCallback]
        private void Update()
        {
            if (_isDashing)
                Dashing();
        }
        [ServerCallback]
        public void ResetDash() =>
            EndDash();
        [Command]
        public void StartDash(float basicSpeed)
        {
            if (_isDashing)
                return;

            _targetPosition = transform.position + transform.forward * _distance;
            _isDashing = true;
            ChangeAnimatorSpeed(_speed / basicSpeed);
        }

        [ClientRpc]
        private void ChangeAnimatorSpeed(float basicSpeed) =>
            _animator.ChangeAnimatorSpeed(basicSpeed);

        [ServerCallback]
        private void Dashing()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            if (transform.position == _targetPosition)
                EndDash();
        }

        [ServerCallback]
        private void EndDash()
        {
            _isDashing = false;
            ChangeAnimatorSpeed(_animator.DefaultAnimatorSpeed);
        }

    }
}