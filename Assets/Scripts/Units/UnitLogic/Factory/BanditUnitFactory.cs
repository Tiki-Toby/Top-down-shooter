using AssetData;
using Units.AnimationLogic;
using Units.AttackLogic;
using Units.LookDirectionLogic;
using Units.MoveLogic;
using Units.UnitDescription;
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
            
            unitController.AddUnitModule(new DefaultAiMoveController(_attackService));
            unitController.AddUnitModule(new AiLookDirectionController(_attackService));
            unitController.AddUnitModule(new AiAttackController(_attackService, _bulletManager));

            return unitController;
        }
    }
}