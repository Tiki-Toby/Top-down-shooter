using GameLogic.UnitLogic;
using UnityEngine;

namespace GameLogic.MoveLogic
{
    public abstract class BaseMoveController : BaseUnitModuleController
    {
        private Vector2 _currentVelocity;
        
        public float CurrentVelocity => _currentVelocity.magnitude;
        public Vector2 MoveDirection => _currentVelocity.normalized;
        public Vector2 CurrentVelocityVector => _currentVelocity;
        public bool IsMovement => CurrentVelocity == 0;

        public BaseMoveController()
        {
            _currentVelocity = Vector2.zero;
        }
        
        public void UpdateMoveData()
        {
            _currentVelocity = UpdateDirection() * UnitController.UnitDataController.MaxVelocity;
        }

        protected abstract Vector2 UpdateDirection();
    }
} 