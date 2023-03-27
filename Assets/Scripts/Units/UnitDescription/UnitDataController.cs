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
        public float HpRatio => _unitData.HealthPoint.Value / _unitDefaultData.MaxHealth.Value;

        public bool IsAlive => _unitData.HealthPoint.Value > 0f;
        public bool IsAttackPossible => _unitData.AttackCooldown.Value <= 0f;

        public UnitDataController(UnitDefaultStructData unitDefaultData,
            UnitAttackStructData unitAttackData)
        {
            _unitDefaultData = new UnitDefaultData(unitDefaultData);
            _unitAttackData = new UnitAttackData(unitAttackData);
            _unitData = new UnitData(unitDefaultData.MaxHealth);
            
            _unitDefaultData.MaxHealth.Subscribe(RecalcHp);
        }

        public void Shoot() => _unitData.AttackCooldown.Value = _unitAttackData.AttackCooldown.Value;
        public void TakeDamage(float damage) => _unitData.HealthPoint.Value -= damage;

        public void Update()
        {
            _unitData.AttackCooldown.Value -= Time.deltaTime;
        }

        public void Reset()
        {
            _unitData.HealthPoint.Value = _unitDefaultData.MaxHealth.Value;
            _unitData.AttackCooldown.Value = 0f;
        }

        private void RecalcHp(float maxHp)
        {
            if (_unitData.HealthPoint.Value > maxHp)
                _unitData.HealthPoint.Value = maxHp;
        }
    }
}