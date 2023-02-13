using System;
using System.Collections.Generic;
using AssetData;

namespace GameLogic.UnitLogic
{
    public class UnitManager
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly Dictionary<EnumUnitType, List<UnitController>> _units;

        public UnitController CharacterUnit
        {
            get
            {
                List<UnitController> characterUnitList = _units[EnumUnitType.Character];
                if (characterUnitList.Count > 0)
                    return characterUnitList[0];

                return null;
            }
        }

        public UnitManager(IGameAssetData gameAssetData)
        {
            _gameAssetData = gameAssetData;
            _units = new Dictionary<EnumUnitType, List<UnitController>>();

            foreach (EnumUnitType unitType in Enum.GetValues(typeof(EnumUnitType)))
            {
                _units.Add(unitType, new List<UnitController>());
            }
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