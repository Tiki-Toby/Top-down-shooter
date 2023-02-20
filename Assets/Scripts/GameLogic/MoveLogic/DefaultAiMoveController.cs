using GameLogic.AttackLogic;
using UnityEngine;

namespace GameLogic.MoveLogic
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
            return Vector2.up;
            //_attackService.TryGetTarget(unit)
            //return Vector2.ClampMagnitude(direction, 1f);
        }
    }
}