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
            if (!isLocalPlayer || _input == null)
                return;

            if (_input.IsSkillClicked())
                _dash.StartDash();

            if (_dash.IsDashing)
                return;

            Vector2 movementAxis = _input.GetMovementAxis();
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

        [Client]
        private void Move(Vector3 direction) =>
           transform.Translate(direction * _speed * Time.deltaTime, Space.World);
        [Client]
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