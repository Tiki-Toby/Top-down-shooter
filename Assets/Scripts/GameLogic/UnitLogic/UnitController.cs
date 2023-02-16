using GameLogic.AnimationLogic;
using GameLogic.AttackLogic;
using GameLogic.LookDirectionLogic;
using GameLogic.MoveLogic;
using GameLogic.UnitDescription;

namespace GameLogic.UnitLogic
{
    public class UnitController
    {
        private readonly IMoveController _moveController;
        private readonly ALookDirectionController _lookDirectionController;
        private readonly BaseAttackController _attackController;
        private readonly ViewController _viewController;
        private readonly UnitDataController _unitDataController;

        public ViewController ViewController => _viewController;
        public UnitDataController UnitDataController => _unitDataController;

        public UnitController(ViewController viewController,
            IMoveController moveController,
            ALookDirectionController lookDirectionController,
            BaseAttackController attackController,
            UnitDataController unitDataController)
        {
            _viewController = viewController;
            _viewController.UnitView.TakeDamage.AddListener(TakeDamage);
            
            _moveController = moveController;
            _lookDirectionController = lookDirectionController;
            _attackController = attackController;
            _unitDataController = unitDataController;
        }

        public void Update()
        {
            Move();
            Attack();
            _lookDirectionController.UpdateLookDirection();
            
            _viewController.Update();
            _unitDataController.Update();
        }
        
        private void Move()
        {
            _moveController.Move();
            _viewController.Move(_moveController);
        }

        private void TakeDamage(float damage)
        {
            _unitDataController.TakeDamage(damage);
        }

        private void Attack()
        {
            if(_unitDataController.IsAlive && _unitDataController.IsAttackPossible)
                _attackController.Attack(ViewController.BulletSpawnPosition, _lookDirectionController.LookDirection, _unitDataController);
        }
    }
}