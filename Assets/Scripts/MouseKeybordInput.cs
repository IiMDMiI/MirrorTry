using UnityEngine;

namespace Gameplay
{
    public class MouseKeybordInput : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";

        public Vector2 GetMovementAxis() =>
            new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

        public Vector2 GetRotationAxis() => 
            new Vector2(Input.GetAxis(MouseX), Input.GetAxis(MouseY));

        public bool IsSkillClicked() => 
            Input.GetMouseButtonUp(0);
    }
}