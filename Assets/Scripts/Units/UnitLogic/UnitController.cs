using Units.AnimationLogic;
using Units.UnitDescription;

namespace Units.UnitLogic
{
    public class UnitController
    {
        private readonly UnitModulesPool _unitModulesPool;
        private readonly ViewController _viewController;
        private readonly UnitDataController _unitDataController;

        public ViewController ViewController => _viewController;
        public UnitDataController UnitDataController => _unitDataController;

        public UnitController(ViewController viewController,
            UnitDataController unitDataController)
        {
            _viewController = viewController;
            _unitDataController = unitDataController;
            _unitModulesPool = new UnitModulesPool(this);
            
            _viewController.UnitView.TakeDamage.AddListener(TakeDamage);
        }

        public void Update()
        {
            _unitModulesPool.Update();
            _viewController.Update();
            _unitDataController.Update();
        }

        public void AddUnitModule<T>(T module) where T : BaseUnitModuleController
        {
            _unitModulesPool.AddUnitModule(module);
        }

        private void TakeDamage(float damage)
        {
            _unitDataController.TakeDamage(damage);
        }
    }
}