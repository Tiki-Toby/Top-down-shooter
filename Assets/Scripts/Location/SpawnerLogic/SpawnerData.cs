using System;
using Units.UnitLogic;
using UnityEngine;

namespace Location.SpawnerLogic
{
    [Serializable]
    public struct SpawnerData
    {
        [SerializeField] private EnumUnitType unitType;
        [SerializeField] private float spawnRadius;
        [SerializeField] private float spawnDelay;
        [SerializeField] private int maxUnitInMomentCount;
        [SerializeField] private int spawnCapacity;

        public EnumUnitType UnitType => unitType;
        public float SpawnRadius => spawnRadius;
        public float SpawnDelay => spawnDelay;
        public int MaxUnitInMomentCount => maxUnitInMomentCount;
        public int SpawnCapacity => spawnCapacity;
    }
}