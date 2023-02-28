using Units.UnitDescription;
using UnityEngine;

namespace Units.AttackLogic
{
    public class InputAttackController : BaseAttackController
    {
        public InputAttackController(BulletManager bulletManager) : base(bulletManager)
        {
        }

        protected override void Attack()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 lookDirection = UnitController.ViewController.UnitLookDirection *
                                        UnitController.UnitDataController.BulletVelocity;
                
                BulletManager.SpawnBullet(UnitController.UnitDataController.BulletViewPrefab, 
                    UnitController.ViewController.BulletSpawnPosition, 
                    lookDirection, 
                    UnitController.UnitDataController.Damage,
                    UnitController.UnitDataController.BulletLifeTime);
                UnitController.UnitDataController.Shoot();
            }
        }
    }
}