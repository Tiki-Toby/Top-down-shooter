using AssetData;
using HairyEngine.HairyCamera;
using Location;
using Units.UnitLogic;
using UnityEngine;

namespace GameLogic.Core
{
    public class LocationManager
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly UnitManager _unitManager;

        private LocationView _currentLocationView;

        public LocationManager(IGameAssetData gameAssetData,
            UnitManager unitManager)
        {
            _gameAssetData = gameAssetData;
            _unitManager = unitManager;
        }

        public void LoadLocation(string locationId)
        { 
            var locationViewPrefab = _gameAssetData.GetLocationObject(locationId);
            LocationView locationView = GameObject.Instantiate<LocationView>(locationViewPrefab);

            if (_currentLocationView != null)
            {
                
            }

            _currentLocationView = locationView;
            Vector3 characterSpawnPoint = locationView.CharacterSpawnPoints["SpawnPoint"].position;
            UnitController characterUnitController = _unitManager.AddUnit(EnumUnitType.Character, characterSpawnPoint);
            
            CameraHandler.Instance.AddTarget(characterUnitController.ViewController.UnitView.transform);

            Vector3 banditPosition = new Vector3(27.5244846f, 3.33341742f, -2.62396741f);
            UnitController banditUnitController = _unitManager.AddUnit(EnumUnitType.Bandit, banditPosition);
        }
    }
}