using BuffLogic;

namespace Units.UnitDescription
{
    public class UnitAttackData
    {
        public readonly BaseBuffableValue<float> AttackCooldown;
        public readonly BaseBuffableValue<float> AgrZoneRadius;
        public readonly BaseBuffableValue<float> Damage;

        public UnitAttackData(UnitAttackStructData attackData)
        {
            AttackCooldown = new BaseBuffableValue<float>(attackData.AttackCooldown);
            AgrZoneRadius = new BaseBuffableValue<float>(attackData.AgrZoneRadius);
            Damage = new BaseBuffableValue<float>(attackData.Damage);
        }
    }
}