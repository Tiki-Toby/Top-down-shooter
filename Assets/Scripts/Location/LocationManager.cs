using AssetData;
using Constants;
using HairyEngine.HairyCamera;
using Location;
using Location.SpawnerLogic;
using Units.UnitLogic;
using UnityEngine;

namespace GameLogic.Core
{
    public class LocationManager
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly UnitManager _unitManager;
        private readonly SpawnerManager _spawnerManager;

        private LocationView _currentLocationView;

        public LocationManager(IGameAssetData gameAssetData,
            UnitManager unitManager)
        {
            _gameAssetData = gameAssetData;
            _unitManager = unitManager;
            _spawnerManager = new SpawnerManager(unitManager);
        }

        public void LoadLocation(string locationId)
        { 
            var locationViewPrefab = _gameAssetData.GetLocationObject(locationId);
            LocationView locationView = Object.Instantiate(locationViewPrefab);

            if (_currentLocationView != null)
                UnloadLocation();

            _currentLocationView = locationView;
            RespawnPlayer();
            _spawnerManager.Init(_currentLocationView.SpawnersObject);
        }

        public void Update()
        {
            _spawnerManager.Update();
        }

        public void UnloadLocation()
        {
            Object.Destroy(_currentLocationView.gameObject);
            _spawnerManager.Dispose();
        }

        public void RespawnPlayer()
        {
            Vector3 characterSpawnPoint = _currentLocationView.CharacterSpawnPoints[ConstantsLocationNames.SpawnPoint].position;
            UnitController characterUnitController = _unitManager.AddUnit(EnumUnitType.Character, characterSpawnPoint);
            characterUnitController.UnitEventController.OnUnitAfterDeadSubscribe(() => 
                CameraHandler.Instance.RemoveTarget(characterUnitController.ViewController.UnitView.transform));
            
            CameraHandler.Instance.AddTarget(characterUnitController.ViewController.UnitView.transform);
        }
    }
}