using System;
using Units.UnitLogic;

namespace Units.UnitDescription
{
    [Serializable]
    public struct UnitDefaultStructData
    {
        public float MaxHealth;
        public float MaxVelocity;
        public EnumUnitType UnitType;
    }
}