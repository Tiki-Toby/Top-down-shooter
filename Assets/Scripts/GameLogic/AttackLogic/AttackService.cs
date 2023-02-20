using System;
using System.Collections.Generic;
using GameLogic.UnitLogic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameLogic.AttackLogic
{
    public class AttackService
    {
        private readonly Dictionary<UnitController, UnitController> _targetsDictionary;

        public AttackService()
        {
            _targetsDictionary = new Dictionary<UnitController, UnitController>();
        }

        public void FindTarget(IEnumerable<UnitController> unitEnumerator, UnitController unit)
        {
            Vector2 unitPosition = unit.ViewController.UnitPosition;
            UnitController target = null;
            float minMagnitude = float.MaxValue;
            
                foreach (var targetUnit in unitEnumerator)
                {
                    float distance = (unitPosition - targetUnit.ViewController.UnitPosition).magnitude;
                    if (distance < minMagnitude)
                    {
                        minMagnitude = distance;
                        target = targetUnit;
                    }
            }

            if (minMagnitude < unit.UnitDataController.AgrZoneRadius && target != null)
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

        public static List<EnumUnitType> UpdateAllowUnitTypes(EnumUnitType unitType, List<EnumUnitType> allowUnitTypes = null)
        {
            if (allowUnitTypes == null)
                allowUnitTypes = new List<EnumUnitType>();
            else
                allowUnitTypes.Clear();
            
            switch (unitType)
            {
                case EnumUnitType.Character:
                    allowUnitTypes.AddRange(Enum.GetValues(typeof(EnumUnitType)));
                    allowUnitTypes.Remove(EnumUnitType.Undefined);
                    break;
                case EnumUnitType.Bandit:
                    allowUnitTypes.Add(EnumUnitType.Character);
                    break;
            }

            return allowUnitTypes;
        }
    }
}