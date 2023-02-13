using UnityEngine;

namespace GameLogic.LookDirectionLogic
{
    public interface ILookDirectionController
    {
        Vector2 LookDirection { get; }
        void UpdateLookDirection();
    }
}