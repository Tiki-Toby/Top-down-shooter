using GameLogic.UnitDescription;
using UnityEngine;

namespace GameLogic.AttackLogic
{
    public abstract class BaseAttackController
    {
        protected readonly BulletManager _bulletManager;
        
        public BaseAttackController(BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
        }
        
        public abstract void Attack(Vector3 bulletSpawnPos, Vector2 lookDirection, UnitDataController unitDataController);
    }
}