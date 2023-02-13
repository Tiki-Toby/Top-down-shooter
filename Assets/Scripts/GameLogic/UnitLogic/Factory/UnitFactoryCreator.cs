using System;
using AssetData;

namespace GameLogic.UnitLogic.Factory
{
    public class UnitFactoryCreator
    {
        private readonly IGameAssetData _gameAssetData;

        private readonly CharacterUnitFactory CharacterUnitFactory;
        
        public UnitFactoryCreator(IGameAssetData gameAssetData)
        {
            _gameAssetData = gameAssetData;
            CharacterUnitFactory = new CharacterUnitFactory(_gameAssetData);
        }

        public BaseUnitFactory GetUnitFactoryByType(EnumUnitType unitType)
        {
            switch (unitType)
            {
                case EnumUnitType.Character:
                    return CharacterUnitFactory;
                case EnumUnitType.Bandit:
                    return CharacterUnitFactory;
                
                default:
                    throw new ArgumentException("Unknown unit type for factory");
            }
        }
    }
}