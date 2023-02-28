using Units.UnitDescription;
using Units.UnitLogic;
using UnityEngine;

namespace Units.AttackLogic
{
    public abstract class BaseAttackController : BaseUnitModuleController
    {
        protected readonly BulletManager BulletManager;

        public bool IsAttackPossible => UnitController.UnitDataController.IsAlive &&
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