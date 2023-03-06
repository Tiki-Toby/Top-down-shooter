using BuffLogic;
using Units.UnitLogic;

namespace Units.UnitDescription
{
    public class UnitDefaultData
    {
        public readonly BaseBuffableValue<float> MaxHealth;
        public readonly BaseBuffableValue<float> MaxVelocity;
        public readonly EnumUnitType UnitType;

        public UnitDefaultData(UnitDefaultStructData data)
        {
            MaxHealth = new BaseBuffableValue<float>(data.MaxHealth);
            MaxVelocity = new BaseBuffableValue<float>(data.MaxVelocity);
            UnitType = data.UnitType;
        }
    }
}