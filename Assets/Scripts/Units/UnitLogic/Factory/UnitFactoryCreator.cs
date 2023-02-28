using System;
using AssetData;
using GameLogic.AttackLogic;

namespace Units.UnitLogic.Factory
{
    public class UnitFactoryCreator
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly BulletManager _bulletManager;
        private readonly AttackService _attackService;

        private readonly CharacterUnitFactory _characterUnitFactory;
        private readonly BanditUnitFactory _banditUnitFactory;
        
        public UnitFactoryCreator(IGameAssetData gameAssetData, 
            BulletManager bulletManager, 
            AttackService attackService)
        {
            _gameAssetData = gameAssetData;
            _bulletManager = bulletManager;
            _attackService = attackService;
            _characterUnitFactory = new CharacterUnitFactory(_gameAssetData, _bulletManager, _attackService);
            _banditUnitFactory = new BanditUnitFactory(_gameAssetData, _bulletManager, _attackService);
        }

        public BaseUnitFactory GetUnitFactoryByType(EnumUnitType unitType)
        {
            switch (unitType)
            {
                case EnumUnitType.Character:
                    return _characterUnitFactory;
                case EnumUnitType.Bandit:
                    return _banditUnitFactory;
                
                default:
                    throw new ArgumentException("Unknown unit type for factory");
            }
        }
    }
}