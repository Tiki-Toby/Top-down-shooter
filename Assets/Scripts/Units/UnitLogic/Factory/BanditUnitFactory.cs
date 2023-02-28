using AssetData;
using GameLogic.AnimationLogic;
using GameLogic.AttackLogic;
using GameLogic.LookDirectionLogic;
using GameLogic.MoveLogic;
using GameLogic.UnitDescription;
using UnityEngine;

namespace Units.UnitLogic.Factory
{
    public class BanditUnitFactory : BaseUnitFactory
    {
        public BanditUnitFactory(IGameAssetData gameAssetData, 
            BulletManager bulletManager, 
            AttackService attackService) : base(gameAssetData, bulletManager, attackService, "BanditPool")
        {
        }

        public override UnitController CreateUnitController(Vector3 position)
        {
            var banditPrefab = _gameAssetData.GetUnitView(EnumUnitType.Bandit);
            UnitView banditInstance = GameObject.Instantiate<UnitView>(banditPrefab, _parentTransform);
            ViewController viewController = new ViewController(banditInstance);
            viewController.SetPosition(position);
            
            var unitDataController = new UnitDataController(banditInstance.UnitDefaultData, banditInstance.UnitAttackData);
            var unitController = new UnitController(viewController, unitDataController);
            
            unitController.AddUnitModule(new InputMoveController());
            unitController.AddUnitModule(new InputLookDirectionController());
            unitController.AddUnitModule(new InputAttackController(_bulletManager));

            return unitController;
        }
    }
}