using AssetData;
using GameFlow.Client.Infrastructure;
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
            
            var unitDataController = new UnitDataController(banditInstance.UnitDefaultData, banditInstance.UnitAttackData);
            var unitController = new UnitController(viewController, unitDataController);
            
            unitController.AddUnitModule(new InputMoveController());
            unitController.AddUnitModule(new InputLookDirectionController());
            unitController.AddUnitModule(new InputAttackController(_bulletManager));

            return unitController;
        }
    }
}