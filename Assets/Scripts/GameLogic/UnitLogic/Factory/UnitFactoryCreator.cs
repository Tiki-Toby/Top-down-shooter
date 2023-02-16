using System;
using AssetData;
using GameLogic.AttackLogic;

namespace GameLogic.UnitLogic.Factory
{
    public class UnitFactoryCreator
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly BulletManager _bulletManager;

        private readonly CharacterUnitFactory CharacterUnitFactory;
        
        public UnitFactoryCreator(IGameAssetData gameAssetData, BulletManager bulletManager)
        {
            _gameAssetData = gameAssetData;
            _bulletManager = bulletManager;
            CharacterUnitFactory = new CharacterUnitFactory(_gameAssetData, _bulletManager);
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