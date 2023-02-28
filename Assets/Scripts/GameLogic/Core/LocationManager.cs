﻿using AssetData;
using GameLogic.UnitLogic;
using HairyEngine.HairyCamera;
using Location;
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
        }
    }
}