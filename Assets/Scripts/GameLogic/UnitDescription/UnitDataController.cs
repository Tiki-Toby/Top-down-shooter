using GameLogic.AttackLogic;
using UnityEngine;

namespace GameLogic.UnitDescription
{
    public class UnitDataController
    {
        private readonly UnitDefaultData _unitDefaultData;
        private readonly UnitAttackData _unitAttackData;
        private readonly UnitData _unitData;

        public float MaxVelocity => _unitDefaultData.MaxVelocity;
        public float Damage => _unitAttackData.Damage;
        public float BulletLifeTime => _unitAttackData.BulletLifeTime;
        public float BulletVelocity => _unitAttackData.BulletVelocity;
        public BulletView BulletViewPrefab => _unitAttackData.BulletViewPrefab;

        public bool IsAlive => _unitData.HealthPoint > 0f;
        public bool IsAttackPossible => _unitData.AttackCooldown <= 0f;

        public UnitDataController(UnitDefaultData unitDefaultData,
            UnitAttackData unitAttackData)
        {
            _unitDefaultData = unitDefaultData;
            _unitAttackData = unitAttackData;
            _unitData = new UnitData(_unitDefaultData.MaxHealth);
        }

        public void Shoot() => _unitData.AttackCooldown = _unitAttackData.AttackCooldown;
        public void TakeDamage(float damage) => _unitData.HealthPoint -= damage;

        public void Update()
        {
            _unitData.AttackCooldown -= Time.deltaTime;
        }
    }
}