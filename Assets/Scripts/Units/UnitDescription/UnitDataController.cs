using BuffLogic;
using UniRx;
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

        public BaseBuffableValue<float> MaxVelocity => _unitDefaultData.MaxVelocity;
        public BaseBuffableValue<float> Damage => _unitAttackData.Damage;
        public BaseBuffableValue<float> AgrZoneRadius => _unitAttackData.AgrZoneRadius;
        public float HpRatio => _unitData.HealthPoint / _unitDefaultData.MaxHealth.Value;

        public bool IsAlive => _unitData.HealthPoint > 0f;
        public bool IsAttackPossible => _unitData.AttackCooldown <= 0f;

        public UnitDataController(UnitDefaultStructData unitDefaultData,
            UnitAttackStructData unitAttackData)
        {
            _unitDefaultData = new UnitDefaultData(unitDefaultData);
            _unitAttackData = new UnitAttackData(unitAttackData);
            _unitData = new UnitData(unitDefaultData.MaxHealth);
            
            _unitDefaultData.MaxHealth.Subscribe(RecalcHp);
        }

        public void Shoot() => _unitData.AttackCooldown = _unitAttackData.AttackCooldown.Value;
        public void TakeDamage(float damage) => _unitData.HealthPoint -= damage;

        public void Update()
        {
            _unitData.AttackCooldown -= Time.deltaTime;
        }

        public void Reset()
        {
            _unitData.HealthPoint = _unitDefaultData.MaxHealth.Value;
            _unitData.AttackCooldown = 0f;
        }

        private void RecalcHp(float maxHp)
        {
            if (_unitData.HealthPoint > maxHp)
                _unitData.HealthPoint = maxHp;
        }
    }
}