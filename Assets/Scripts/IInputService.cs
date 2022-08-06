using UnityEngine;

namespace Gameplay
{
    public interface IInputService
    {
        public Vector2 GetMovementAxis();
        public Vector2 GetRotationAxis();
        public bool IsSkillClicked();

    }
}