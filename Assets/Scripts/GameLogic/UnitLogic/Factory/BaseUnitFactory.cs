using AssetData;
using UnityEngine;

namespace GameLogic.UnitLogic.Factory
{
    public abstract class BaseUnitFactory
    {
        protected readonly IGameAssetData _gameAssetData;
        protected readonly Transform _parentTransform;

        public BaseUnitFactory(IGameAssetData gameAssetData, string parentName)
        {
            _gameAssetData = gameAssetData;
            _parentTransform = new GameObject().transform;
        }
        
        public abstract UnitController CreateUnitController();
    }
}