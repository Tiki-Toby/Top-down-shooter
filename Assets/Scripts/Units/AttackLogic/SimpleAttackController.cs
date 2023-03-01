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
                                  UnitController.UnitDataController.BulletVelocity;

            BulletManager.SpawnBullet(UnitController.UnitDataController.BulletViewPrefab,
                UnitController.ViewController.BulletSpawnPosition,
                bulletForce,
                UnitController.UnitDataController.Damage,
                UnitController.UnitDataController.BulletLifeTime);
            UnitController.UnitDataController.Shoot();
        }
    }
}