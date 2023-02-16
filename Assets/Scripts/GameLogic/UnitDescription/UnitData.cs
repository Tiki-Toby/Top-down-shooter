namespace GameLogic.UnitDescription
{
    public class UnitData
    {
        public float HealthPoint;
        public float AttackCooldown;

        public UnitData(float healthPoint)
        {
            HealthPoint = healthPoint;
            AttackCooldown = 0f;
        }
    }
}