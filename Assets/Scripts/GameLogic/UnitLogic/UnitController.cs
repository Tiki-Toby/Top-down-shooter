using GameLogic.AnimationLogic;
using GameLogic.AttackLogic;
using GameLogic.LookDirectionLogic;
using GameLogic.MoveLogic;

namespace GameLogic.UnitLogic
{
    public class UnitController
    {
        private readonly IMoveController _moveController;
        private readonly ALookDirectionController _lookDirectionController;
        private readonly IAttackController _attackController;
        private readonly ViewController _viewController;

        public ViewController ViewController => _viewController;

        public UnitController(ViewController viewController,
            IMoveController moveController,
            ALookDirectionController lookDirectionController,
            IAttackController attackController)
        {
            _viewController = viewController;
            _moveController = moveController;
            _lookDirectionController = lookDirectionController;
            _attackController = attackController;
        }

        public void Update()
        {
            Move();
            Attack();
            _lookDirectionController.UpdateLookDirection();
            _viewController.Update();
        }
        
        private void Move()
        {
            _moveController.Move();
            _viewController.Move(_moveController);
        }

        private void Attack()
        {
            _attackController.Attack();
        }
    }
}