namespace Units.AttackLogic
{
    public class AiAttackController : SimpleAttackController
    {
        private readonly AttackService _attackService;
        
        public AiAttackController(AttackService attackService, BulletManager bulletManager) : base(bulletManager)
        {
            _attackService = attackService;
        }

        protected override bool IsAdditionalAttackConditionHandle()
        {
            return _attackService.TryGetTarget(UnitController, out var _);
        }
    }
}