using System;
using System.Collections.Generic;
using System.Linq;
using Units.UnitLogic;
using UnityEngine;

namespace Units.AttackLogic
{
    public class AttackService
    {
        private UnitManager _unitManager;
        private readonly Dictionary<UnitController, UnitController> _targetsDictionary;
        private readonly Dictionary<EnumUnitType, List<EnumUnitType>> _allowsTargetDictionary;

        public AttackService()
        {
            _targetsDictionary = new Dictionary<UnitController, UnitController>();
            _allowsTargetDictionary = GetAllowUnitTypes();
        }

        public void Init(UnitManager unitManager)
        {
            _unitManager = unitManager;
        }

        public void FindTarget(UnitController unit)
        {
            Vector2 unitPosition = unit.ViewController.UnitPosition;
            UnitController target = null;
            float minMagnitude = float.MaxValue;

            foreach (var allowType in _allowsTargetDictionary[unit.UnitDataController.UnitType])
            {
                foreach (var targetUnit in _unitManager[allowType])
                {
                    float distance = (unitPosition - targetUnit.ViewController.UnitPosition).magnitude;
                    if (distance < minMagnitude)
                    {
                        minMagnitude = distance;
                        target = targetUnit;
                    }
                }
            }

            if (minMagnitude < unit.UnitDataController.AgrZoneRadius.Value && target != null)
                _targetsDictionary.Add(unit, target);
        }
        
        public bool TryGetTarget(UnitController unit, out UnitController target)
        {
            return _targetsDictionary.TryGetValue(unit, out target);
        }

        public void Reset()
        {
            _targetsDictionary.Clear();
        }

#warning LegacyCode: need config prefab
        //TODO: added prefab config for allow unit types
        public static Dictionary<EnumUnitType, List<EnumUnitType>> GetAllowUnitTypes()
        {
            Dictionary<EnumUnitType, List<EnumUnitType>> dictionaryAllowUnitTypes = new Dictionary<EnumUnitType, List<EnumUnitType>>();
            List<EnumUnitType> allUnitTypes = Enum.GetValues(typeof(EnumUnitType)).Cast<EnumUnitType>().ToList();
            allUnitTypes.Remove(EnumUnitType.Undefined);
            
            //Character
            List<EnumUnitType> allowUnitTypes = new List<EnumUnitType>(allUnitTypes);
            allowUnitTypes.Remove(EnumUnitType.Character);
            dictionaryAllowUnitTypes.Add(EnumUnitType.Character, allowUnitTypes);
            
            //Bandit
            allowUnitTypes = new List<EnumUnitType>();
            allowUnitTypes.Add(EnumUnitType.Character);
            dictionaryAllowUnitTypes.Add(EnumUnitType.Bandit, allowUnitTypes);

            return dictionaryAllowUnitTypes;
        }
    }
}