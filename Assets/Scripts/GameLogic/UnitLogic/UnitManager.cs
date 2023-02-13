using System;
using System.Collections.Generic;
using AssetData;
using GameLogic.UnitLogic.Factory;
using Tools;
using UnityEngine;

namespace GameLogic.UnitLogic
{
    public class UnitManager
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly UnitFactoryCreator _unitFactoryCreator;
        private readonly Dictionary<EnumUnitType, UnitsPool> _units;

        public UnitController CharacterUnit
        {
            get
            {
                UnitsPool characterUnitList = _units[EnumUnitType.Character];
                if (characterUnitList.ActiveCount == 0)
                    throw new Exception("Missed character unit, but still try to get access");

                return characterUnitList.FirstActive;
            }
        }

        public UnitManager(IGameAssetData gameAssetData)
        {
            _gameAssetData = gameAssetData;
            _units = new Dictionary<EnumUnitType, UnitsPool>();
            _unitFactoryCreator = new UnitFactoryCreator(_gameAssetData);

            foreach (EnumUnitType unitType in Enum.GetValues(typeof(EnumUnitType)))
            {
                BaseUnitFactory factory = _unitFactoryCreator.GetUnitFactoryByType(unitType);
                _units.Add(unitType, new UnitsPool(factory));
            }
        }

        public UnitController AddUnit(EnumUnitType unitType, Vector3 position)
        {
            return _units[unitType].GetNextController(position);
        }

        public void Update()
        {
            foreach (var units in _units.Values)
            {
                foreach (var unitController in units)
                {
                    unitController.Update();
                }
            }
        }
    }
}