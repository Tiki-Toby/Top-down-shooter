using System;
using GameLogic.AttackLogic;

namespace GameLogic.UnitDescription
{
    [Serializable]
    public struct UnitAttackData
    {
        public float AttackCooldown;
        public float AgrZoneRadius;
        public float Damage;
        
        public float BulletLifeTime;
        public float BulletVelocity;
        public BulletView BulletViewPrefab;
    }
}