﻿using System;
using System.Collections;
using System.Collections.Generic;
using AssetData;
using GameLogic.AttackLogic;
using GameLogic.UnitLogic.Factory;
using Tools;
using UnityEngine;

namespace GameLogic.UnitLogic
{
    public class UnitManager : IEnumerable<UnitController>
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly AttackService _attackService;
        private readonly UnitFactoryCreator _unitFactoryCreator;
        private readonly Dictionary<EnumUnitType, UnitsPool> _units;

        public UnitsPool this[EnumUnitType type] => _units[type];

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

        public UnitManager(AttackService attackService, 
            UnitFactoryCreator unitFactoryCreator)
        {
            _attackService = attackService;
            _unitFactoryCreator = unitFactoryCreator;
            
            _units = new Dictionary<EnumUnitType, UnitsPool>();

            foreach (EnumUnitType unitType in Enum.GetValues(typeof(EnumUnitType)))
            {
                if(unitType == EnumUnitType.Undefined)
                    continue;
                
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
            _attackService.Reset();
            foreach (var unit in this)
            {
                _attackService.FindTarget(this, unit);
            }
            foreach (var unit in this)
            {
                unit.Update();
            }
        }

        public IEnumerator<UnitController> GetEnumerator()
        {
            foreach (var unitPool in _units.Values)
                foreach (var unit in unitPool)
                    yield return unit;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}