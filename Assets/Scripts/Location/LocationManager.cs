using AssetData;
using AssetManagement;
using GameFlow.Client.Infrastructure;
using Units.UnitLogic;
using HairyEngine.HairyCamera;
using UnityEngine;

namespace Location
{
    public class LocationManager
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly IAssetInstantiator _assetInstantiator;
        private readonly UnitManager _unitManager;

        private LocationView _currentLocationView;

        public LocationManager(IGameAssetData gameAssetData,
            IAssetInstantiator assetInstantiator,
            UnitManager unitManager)
        {
            _gameAssetData = gameAssetData;
            _assetInstantiator = assetInstantiator;
            _unitManager = unitManager;
        }

        public void LoadLocation(string locationId)
        { 
            ObjectReference locationViewPrefab = _gameAssetData.GetLocationObject(locationId);
            LocationView locationView = _assetInstantiator.Instantiate<LocationView>(locationViewPrefab);

            if (_currentLocationView != null)
            {
                
            }

            _currentLocationView = locationView;
            Vector3 characterSpawnPoint = locationView.CharacterSpawnPoints["SpawnPoint"].position;
            UnitController characterUnitController = _unitManager.AddUnit(EnumUnitType.Character, characterSpawnPoint);
            
            CameraHandler.Instance.AddTarget(characterUnitController.ViewController.UnitView.transform);
        }
    }
}