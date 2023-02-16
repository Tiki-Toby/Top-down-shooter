using AssetData;
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
        public CharacterUnitFactory(IGameAssetData gameAssetData, BulletManager bulletManager) : base(gameAssetData, bulletManager, "CharacterPool")
        {
        }

        public override UnitController CreateUnitController(Vector3 position)
        {
            UnitView characterPrefab = _gameAssetData.GetUnitView(EnumUnitType.Character);
            UnitView characterInstance = GameObject.Instantiate(characterPrefab, _parentTransform);
            ViewController viewController = new ViewController(characterInstance);
            viewController.SetPosition(position);
            
            var moveController = new InputMoveController(characterInstance.UnitDefaultData.MaxVelocity);
            var lookController = new InputLookDirectionController(viewController);
            var attackController = new InputAttackController(_bulletManager);

            var unitDataController = new UnitDataController(characterInstance.UnitDefaultData, characterInstance.UnitAttackData);
            
            return new UnitController(viewController, moveController, lookController, attackController, unitDataController);
        }
    }
}