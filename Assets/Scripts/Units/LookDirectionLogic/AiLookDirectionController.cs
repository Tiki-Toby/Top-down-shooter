using Units.AttackLogic;
using Units.UnitLogic;
using UnityEngine;

namespace Units.LookDirectionLogic
{
    public class AiLookDirectionController : BaseLookDirectionController
    {
        private readonly AttackService _attackService;

        public AiLookDirectionController(AttackService attackService)
        {
            _attackService = attackService;
        }
        
        public override void Invoke()
        {
            if(!_attackService.TryGetTarget(UnitController, out UnitController targetController))
                return;
            
            Vector2 unitPosition = UnitController.ViewController.UnitPosition; 
            Vector2 targetPosition = targetController.ViewController.UnitPosition; 
            SetLookDirection((targetPosition - unitPosition).normalized);
        }
    }
}