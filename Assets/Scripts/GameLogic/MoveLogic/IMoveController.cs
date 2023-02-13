using UnityEngine;

namespace GameLogic.MoveLogic
{
    public interface IMoveController
    {
        float CurrentVelocity { get; }
        Vector2 MoveDirection { get; }
        Vector2 CurrentVelocityVector { get; }
        bool IsMovement { get; }
        float MaxVelocity { get; }

        void Move();
    }
} 