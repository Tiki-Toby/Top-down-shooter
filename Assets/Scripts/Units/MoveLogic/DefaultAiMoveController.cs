using Units.AttackLogic;
using Units.UnitLogic;
using UnityEngine;

namespace Units.MoveLogic
{
    public class DefaultAiMoveController : BaseMoveController
    {
        private readonly AttackService _attackService;
        
        public DefaultAiMoveController(AttackService attackService) : base()
        {
            _attackService = attackService;
        } 
        
        protected override Vector2 UpdateDirection()
        {
            if (!_attackService.TryGetTarget(UnitController, out UnitController targetController))
                return Vector2.zero;
            
            Vector2 unitPosition = UnitController.ViewController.UnitPosition; 
            Vector2 targetPosition = targetController.ViewController.UnitPosition;
            Vector2 distance = targetPosition - unitPosition;

            if (distance.magnitude < 4f)
                return Vector2.zero;

            return distance.normalized;
        }
    }
}