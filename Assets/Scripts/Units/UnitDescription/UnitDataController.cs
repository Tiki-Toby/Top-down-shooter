using Units.AttackLogic;
using Units.UnitLogic;
using UnityEngine;

namespace Units.UnitDescription
{
    public class UnitDataController
    {
        private readonly UnitDefaultData _unitDefaultData;
        private readonly UnitAttackData _unitAttackData;
        private readonly UnitData _unitData;

        public EnumUnitType UnitType => _unitDefaultData.UnitType;
        
        public float MaxVelocity => _unitDefaultData.MaxVelocity;
        public float Damage => _unitAttackData.Damage;
        public float AgrZoneRadius => _unitAttackData.AgrZoneRadius;
        public float BulletLifeTime => _unitAttackData.BulletLifeTime;
        public float BulletVelocity => _unitAttackData.BulletVelocity;
        public BulletView BulletViewPrefab => _unitAttackData.BulletViewPrefab;
        public float HpRatio => _unitData.HealthPoint / _unitDefaultData.MaxHealth;

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