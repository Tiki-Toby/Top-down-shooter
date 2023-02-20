using AssetData;
using GameFlow.Client.Infrastructure;
using GameLogic.AttackLogic;
using UnityEngine;

namespace GameLogic.UnitLogic.Factory
{
    public abstract class BaseUnitFactory
    {
        protected readonly IGameAssetData _gameAssetData;
        protected readonly IAssetInstantiator _assetInstantiator;
        protected readonly BulletManager _bulletManager;
        protected readonly AttackService _attackService;
        protected readonly Transform _parentTransform;

        public BaseUnitFactory(IGameAssetData gameAssetData,
            IAssetInstantiator assetInstantiator,
            BulletManager bulletManager, 
            AttackService attackService, 
            string parentName)
        {
            _gameAssetData = gameAssetData;
            _assetInstantiator = assetInstantiator;
            _bulletManager = bulletManager;
            _attackService = attackService;
            _parentTransform = new GameObject(parentName).transform;
        }
        
        public abstract UnitController CreateUnitController(Vector3 position);
    }
}