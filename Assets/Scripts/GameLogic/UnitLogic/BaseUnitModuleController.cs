using System;

namespace GameLogic.UnitLogic
{
    public abstract class BaseUnitModuleController : IDisposable
    {
        private UnitController _unitController;
        public UnitController UnitController => _unitController;

        public void Init(UnitController unitController)
        {
            _unitController = unitController;
        }
        
        public void Dispose()
        {
        }
    }
}