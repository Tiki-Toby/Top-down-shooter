using Units.UnitLogic;

namespace Units.AttackLogic
{
    public abstract class BaseAttackController : BaseUnitModuleController
    {
        protected readonly BulletManager BulletManager;

        public virtual bool IsAttackPossible => UnitController.UnitDataController.IsAlive &&
                                        UnitController.UnitDataController.IsAttackPossible;
        
        public BaseAttackController(BulletManager bulletManager)
        {
            BulletManager = bulletManager;
        }

        public override void Invoke()
        {
            if(IsAttackPossible)
            {
                Attack();
                UnitController.ViewController.Attack();                
            }
        }
        
        protected abstract void Attack();
    }
}