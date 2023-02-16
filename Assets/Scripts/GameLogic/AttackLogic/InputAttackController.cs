using GameLogic.UnitDescription;
using UnityEngine;

namespace GameLogic.AttackLogic
{
    public class InputAttackController : BaseAttackController
    {
        public InputAttackController(BulletManager bulletManager) : base(bulletManager)
        {
        }

        public override void Attack(Vector3 bulletSpawnPos, Vector2 lookDirection, UnitDataController unitDataController)
        {
            if (Input.GetMouseButton(0))
            {
                _bulletManager.SpawnBullet(unitDataController.BulletViewPrefab, 
                    bulletSpawnPos, 
                    lookDirection * unitDataController.BulletVelocity, 
                    unitDataController.Damage,
                    unitDataController.BulletLifeTime);
                unitDataController.Shoot();
            }
        }
    }
}