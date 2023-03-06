﻿using System;
using Units.AttackLogic;

namespace Units.UnitDescription
{
    [Serializable]
    public struct UnitAttackStructData
    {
        public float AttackCooldown;
        public float AgrZoneRadius;
        public float Damage;
        
        public float BulletLifeTime;
        public float BulletVelocity;
        public BulletView BulletViewPrefab;
    }
}