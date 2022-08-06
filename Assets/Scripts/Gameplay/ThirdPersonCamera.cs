using UnityEngine;
using Mirror;

namespace Gameplay
{
    public class ThirdPersonCamera : NetworkBehaviour
    {
        private const int MinXAngle = 190;
        private const int MaxXAngle = 210;
        
        private Vector3 _angles;
        private float _radius = 8;
        private float _sensitivity = 500;
        private IInputService _input;
        private Transform _camera;
        private Transform _player;

        public void Initialize(IInputService input, Transform player, Transform camera)
        {
            _input = input;
            _player = player;
            _camera = camera;
        }

        public void SetCameraLookBehindCharacter()
        {
            if (!isLocalPlayer)
                return;
          
            _angles = Quaternion.LookRotation(-transform.position.normalized).eulerAngles;
            _angles.x = MaxXAngle;
            _camera.position = _player.position + GetRotationVector();
            _camera.LookAt(_player);
        }

        [Client]
        private void Update()
        {
            if (!hasAuthority)
                return;

            if (_input == null)
                return;

            Vector2 rotationAxis = _input.GetRotationAxis();
            Rotate(rotationAxis);
        }
        private void Rotate(Vector2 rotationAxis)
        {
            AdjustAngles(rotationAxis);
            Vector3 vector = GetRotationVector();
            _camera.position = _player.position + (vector);
            _camera.LookAt(_player);
        }

        private Vector3 GetRotationVector()
        {
            Vector3 vector = Vector3.forward;
            Quaternion rotation = Quaternion.Euler(_angles.x, _angles.y, 0);
            return (rotation * vector) * _radius;
        }
        private void AdjustAngles(Vector2 rotationAxis)
        {
            _angles.y += rotationAxis.x * Time.deltaTime * _sensitivity;
            _angles.x += rotationAxis.y * Time.deltaTime * _sensitivity;
            _angles.x = Mathf.Clamp(_angles.x, MinXAngle, MaxXAngle);
        }
    }
}
