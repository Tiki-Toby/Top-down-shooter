﻿using AssetData;
using GameLogic.AnimationLogic;
using GameLogic.AttackLogic;
using GameLogic.LookDirectionLogic;
using GameLogic.MoveLogic;
using GameLogic.UnitDescription;
using UnityEngine;

namespace GameLogic.UnitLogic.Factory
{
    public class CharacterUnitFactory : BaseUnitFactory
    {
        public CharacterUnitFactory(IGameAssetData gameAssetData, 
            BulletManager bulletManager,
            AttackService attackService) : base(gameAssetData, bulletManager, attackService, "CharacterPool")
        {
        }

        public override UnitController CreateUnitController(Vector3 position)
        {
            var banditPrefab = _gameAssetData.GetUnitView(EnumUnitType.Character);
            UnitView characterInstance = GameObject.Instantiate<UnitView>(banditPrefab, _parentTransform);
            ViewController viewController = new ViewController(characterInstance);
            viewController.SetPosition(position);
            
            var moveController = new InputMoveController();
            var lookController = new InputLookDirectionController(viewController);
            var attackController = new InputAttackController(_bulletManager);

            var unitDataController = new UnitDataController(characterInstance.UnitDefaultData, characterInstance.UnitAttackData);
            
            return new UnitController(viewController, moveController, lookController, attackController, unitDataController);
        }
    }
}