using AssetData;
using GameLogic.AttackLogic;
using UnityEngine;

namespace GameLogic.UnitLogic.Factory
{
    public abstract class BaseUnitFactory
    {
        protected readonly IGameAssetData _gameAssetData;
        protected readonly BulletManager _bulletManager;
        protected readonly Transform _parentTransform;

        public BaseUnitFactory(IGameAssetData gameAssetData, BulletManager bulletManager, string parentName)
        {
            _gameAssetData = gameAssetData;
            _bulletManager = bulletManager;
            _parentTransform = new GameObject(parentName).transform;
        }
        
        public abstract UnitController CreateUnitController(Vector3 position);
    }
}