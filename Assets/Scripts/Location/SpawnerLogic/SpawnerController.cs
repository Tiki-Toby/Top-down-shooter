using Units.UnitLogic;
using UnityEngine;

namespace Location.SpawnerLogic
{
    public class SpawnerController
    {
        private readonly UnitManager _unitManager;
        private readonly SpawnerData _spawnerData;
        private readonly Vector3 _spawnPoint;

        private int _spawnedUnitCount;
        private int _currentUnitCount;
        private float _nextSpawnTime;

        public bool IsAlive => _spawnerData.SpawnCapacity == 0 || _spawnedUnitCount < _spawnerData.SpawnCapacity;
        private bool IsSpawnPossible => _currentUnitCount < _spawnerData.MaxUnitInMomentCount &&
                                        IsAlive &&
                                        _nextSpawnTime <= Time.time;
        
        public SpawnerController(UnitManager unitManager, SpawnerView spawnerView)
        {
            _unitManager = unitManager;
            _spawnerData = spawnerView.SpawnerData;
            _spawnPoint = spawnerView.transform.position;

            _spawnedUnitCount = 0;
            _currentUnitCount = 0;
            _nextSpawnTime = Time.time;
            Object.Destroy(spawnerView.gameObject);
        }

        public void Update()
        {
            if (IsSpawnPossible)
                Spawn();
        }
        
        private void Spawn()
        {
            Vector3 spawnPoint = _spawnPoint + (Vector3)Random.insideUnitCircle * _spawnerData.SpawnRadius;
            UnitController unit = _unitManager.AddUnit(_spawnerData.UnitType, spawnPoint);
            unit.UnitEventController.OnUnitAfterDeadSubscribe(() => DestroyUnit(unit));
            _currentUnitCount++;
            _spawnedUnitCount++;
            _nextSpawnTime = Time.time + _spawnerData.SpawnDelay;
        }

        private void DestroyUnit(UnitController unitController)
        {
            _currentUnitCount--;
        }
    }
}