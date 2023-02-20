using System;
using GameLogic.UnitLogic;

namespace GameLogic.UnitDescription
{
    [Serializable]
    public struct UnitDefaultData
    {
        public float MaxHealth;
        public float MaxVelocity;
        public EnumUnitType UnitType;
    }
}