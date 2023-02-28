namespace Units.UnitLogic
{
    public abstract class BaseUnitModuleController
    {
        private UnitController _unitController;
        public UnitController UnitController => _unitController;

        public void Init(UnitController unitController)
        {
            _unitController = unitController;
            Init();
        }

        public virtual void Init(){ }
        
        public abstract void Invoke();
    }
}