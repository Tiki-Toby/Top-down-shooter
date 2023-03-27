using BuffLogic;

namespace Units.UnitDescription
{
    public class UnitData
    {
        public BaseBuffableValue<float> HealthPoint;
        public BaseBuffableValue<float> AttackCooldown;

        public UnitData(float healthPoint)
        {
            HealthPoint = new BaseBuffableValue<float>(healthPoint);
            AttackCooldown = new BaseBuffableValue<float>(0f);
        }
    }
}