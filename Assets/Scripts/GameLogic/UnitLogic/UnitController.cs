using GameLogic.AnimationLogic;
using GameLogic.AttackLogic;
using GameLogic.LookDirectionLogic;
using GameLogic.MoveLogic;

namespace GameLogic.UnitLogic
{
    public class UnitController
    {
        private readonly IMoveController _moveController;
        private readonly ILookDirectionController _lookDirectionController;
        private readonly IAttackController _attackController;
        private readonly ViewController _viewController;

        public UnitController(ViewController viewController,
            IMoveController moveController,
            ILookDirectionController lookDirectionController,
            IAttackController attackController)
        {
            _viewController = viewController;
            _moveController = moveController;
            _lookDirectionController = lookDirectionController;
            _attackController = attackController;
        }

        public void Move()
        {
            _moveController.Move();
            _viewController.Move(_moveController);
        }

        public void LookDirection()
        {
            _lookDirectionController.UpdateLookDirection();
            _viewController.SetLookDirection(_lookDirectionController.LookDirection);
        }

        public void Attack()
        {
            _attackController.Attack();
        }
        
        //public abstract void Buff(Buff buff);

        public void Update()
        {
            _viewController.Update();
        }
    }
}