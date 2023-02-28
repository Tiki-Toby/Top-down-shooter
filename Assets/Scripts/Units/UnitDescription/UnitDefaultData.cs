using System;
using Units.UnitLogic;

namespace Units.UnitDescription
{
    [Serializable]
    public struct UnitDefaultData
    {
        public float MaxHealth;
        public float MaxVelocity;
        public EnumUnitType UnitType;
    }
}