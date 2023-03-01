using System;
using AssetData;
using Units.AttackLogic;

namespace Units.UnitLogic.Factory
{
    public class UnitFactoryCreator
    {
        private readonly CharacterUnitFactory _characterUnitFactory;
        private readonly BanditUnitFactory _banditUnitFactory;
        
        public UnitFactoryCreator(IGameAssetData gameAssetData, 
            BulletManager bulletManager, 
            AttackService attackService)
        {
            _characterUnitFactory = new CharacterUnitFactory(gameAssetData, bulletManager, attackService);
            _banditUnitFactory = new BanditUnitFactory(gameAssetData, bulletManager, attackService);
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