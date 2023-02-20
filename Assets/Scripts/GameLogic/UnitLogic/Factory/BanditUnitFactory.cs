﻿using AssetData;
using GameFlow.Client.Infrastructure;
using GameLogic.AnimationLogic;
using GameLogic.AttackLogic;
using GameLogic.LookDirectionLogic;
using GameLogic.MoveLogic;
using GameLogic.UnitDescription;
using UnityEngine;

namespace GameLogic.UnitLogic.Factory
{
    public class BanditUnitFactory : BaseUnitFactory
    {
        public BanditUnitFactory(IGameAssetData gameAssetData, 
            IAssetInstantiator assetInstantiator,
            BulletManager bulletManager, 
            AttackService attackService) : base(gameAssetData, assetInstantiator, bulletManager, attackService, "BanditPool")
        {
        }

        public override UnitController CreateUnitController(Vector3 position)
        {
            var banditPrefab = _gameAssetData.GetUnitView(EnumUnitType.Bandit);
            UnitView banditInstance = _assetInstantiator.Instantiate<UnitView>(banditPrefab, _parentTransform);
            ViewController viewController = new ViewController(banditInstance);
            viewController.SetPosition(position);
            
            var moveController = new InputMoveController();
            var lookController = new InputLookDirectionController(viewController);
            var attackController = new InputAttackController(_bulletManager);

            var unitDataController = new UnitDataController(banditInstance.UnitDefaultData, banditInstance.UnitAttackData);
            
            return new UnitController(viewController, moveController, lookController, attackController, unitDataController);
        }
    }
}