using UnityEngine;

namespace Units.AttackLogic
{
    public abstract class SimpleAttackController : BaseAttackController
    {
        public override bool IsAttackPossible => base.IsAttackPossible && IsAdditionalAttackConditionHandle();
        
        public SimpleAttackController(BulletManager bulletManager) : base(bulletManager)
        {
        }

        protected abstract bool IsAdditionalAttackConditionHandle();

        protected override void Attack()
        {
            Vector2 bulletForce = UnitController.ViewController.UnitLookDirection *
                                  UnitController.ViewController.BulletView.DefaultVelocity;

            BulletManager.SpawnBullet(UnitController.ViewController.BulletView,
                UnitController.ViewController.BulletSpawnPosition,
                bulletForce,
                UnitController.UnitDataController.Damage.Value,
                UnitController.ViewController.BulletView.LifeTime);
            UnitController.UnitDataController.Shoot();
        }
    }
}