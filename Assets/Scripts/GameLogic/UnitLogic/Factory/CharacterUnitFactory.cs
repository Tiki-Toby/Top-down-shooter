using AssetData;
using GameLogic.AnimationLogic;
using GameLogic.AttackLogic;
using GameLogic.LookDirectionLogic;
using GameLogic.MoveLogic;
using UnityEngine;

namespace GameLogic.UnitLogic.Factory
{
    public class CharacterUnitFactory : BaseUnitFactory
    {
        public CharacterUnitFactory(IGameAssetData gameAssetData) : base(gameAssetData, "CharacterPool")
        {
        }

        public override UnitController CreateUnitController()
        {
            UnitView characterPrefab = _gameAssetData.GetUnitView(EnumUnitType.Character);
            UnitView characterInstance = GameObject.Instantiate(characterPrefab, _parentTransform);
            ViewController viewController = new ViewController(characterInstance);
            
            var moveController = new InputMoveController();
            var lookController = new InputLookDirectionController();
            var attackController = new InputAttackController();
            return new UnitController(viewController, moveController, lookController, attackController);
        }
    }
}