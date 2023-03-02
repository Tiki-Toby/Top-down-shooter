using Units.AnimationLogic;
using Units.UnitDescription;

namespace Units.UnitLogic
{
    public class UnitController
    {
        private readonly UnitModulesPool _unitModulesPool;
        private readonly ViewController _viewController;
        private readonly UnitDataController _unitDataController;
        private readonly UnitEventController _unitEventController;

        public ViewController ViewController => _viewController;
        public UnitDataController UnitDataController => _unitDataController;
        public UnitEventController UnitEventController => _unitEventController;

        public UnitController(ViewController viewController,
            UnitDataController unitDataController)
        {
            _viewController = viewController;
            _unitDataController = unitDataController;
            _unitModulesPool = new UnitModulesPool(this);
            _unitEventController = new UnitEventController(this);
            
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

        public void Reset()
        {
            _unitDataController.Reset();
            _unitEventController.Reset();
        }

        private void TakeDamage(float damage)
        {
            _unitDataController.TakeDamage(damage);
            _unitEventController.TakeDamage(damage);
            
            if(!_unitDataController.IsAlive)
                _unitEventController.BeforeDestroy();
        }
    }
}