using System;
using AssetData;
using GameFlow.Client.Infrastructure;
using GameLogic.AttackLogic;

namespace GameLogic.UnitLogic.Factory
{
    public class UnitFactoryCreator
    {
        private readonly IGameAssetData _gameAssetData;
        private readonly IAssetInstantiator _assetInstantiator;
        private readonly BulletManager _bulletManager;
        private readonly AttackService _attackService;

        private readonly CharacterUnitFactory _characterUnitFactory;
        private readonly BanditUnitFactory _banditUnitFactory;
        
        public UnitFactoryCreator(IGameAssetData gameAssetData, 
            IAssetInstantiator assetInstantiator, 
            BulletManager bulletManager, 
            AttackService attackService)
        {
            _gameAssetData = gameAssetData;
            _assetInstantiator = assetInstantiator;
            _bulletManager = bulletManager;
            _attackService = attackService;
            _characterUnitFactory = new CharacterUnitFactory(_gameAssetData, _assetInstantiator, _bulletManager, _attackService);
            _banditUnitFactory = new BanditUnitFactory(_gameAssetData, _assetInstantiator, _bulletManager, _attackService);
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