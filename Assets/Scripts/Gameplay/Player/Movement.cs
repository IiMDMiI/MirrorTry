using UnityEngine;
using Mirror;

namespace Gameplay
{
    public class Movement : NetworkBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        private Camera _camera;
        private IInputService _input;
        private PlayerAnimator _animator;
        private Dash _dash;

        public void Initialize(IInputService input, PlayerAnimator animator, Dash dash, Camera camera)
        {
            _input = input;
            _animator = animator;
            _dash = dash;
            _camera = camera;
        }


        [Client]
        private void Update()
        {
            if (!isLocalPlayer)
                return;

            if (_input == null)
                return;

            Vector2 movementAxis = _input.GetMovementAxis();

            if (_input.IsSkillClicked())
                _dash.StartDash(_speed);

            if (_dash.IsDashing)
            {
                _animator.ChangeAnimation(AnimationState.Run);
                return;
            }

            if (movementAxis.sqrMagnitude > 0)
            {
                Vector3 direction = GetMovementDirection(movementAxis);
                Move(direction);
                Rotate(direction);
                _animator.ChangeAnimation(AnimationState.Run);
                return;
            }
            _animator.ChangeAnimation(AnimationState.Idle);
        }

        [Command]
        private void Move(Vector3 direction) =>
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);
        [Command]
        private void Rotate(Vector3 direction) =>
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);

        private Vector3 GetMovementDirection(Vector2 movementAxis)
        {
            Vector3 direction = _camera.transform.TransformDirection(movementAxis);
            direction.y = 0;
            direction.Normalize();
            return direction;
        }
    }
}