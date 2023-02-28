using System;
using System.Collections.Generic;

namespace Units.UnitLogic
{
    public class UnitModulesPool : IDisposable
    {
        private readonly Dictionary<Type, BaseUnitModuleController> _unitModules;
        private readonly UnitController _unitController;

        public UnitModulesPool(UnitController unitController)
        {
            _unitController = unitController;
            _unitModules = new Dictionary<Type, BaseUnitModuleController>();
        }

        public void Update()
        {
            foreach (var unitModule in _unitModules.Values)
            {  
                unitModule.Invoke();
            }
        }

        public void AddUnitModule<T>(T unitModule) where T : BaseUnitModuleController
        {
            unitModule.Init(_unitController);
            _unitModules.Add(typeof(T), unitModule);
        }

        public bool TryGetUnitModule<T>(out T unitModule) where T : BaseUnitModuleController
        {
            unitModule = null;
            if (!_unitModules.TryGetValue(typeof(T), out var unitModuleBase))
                return false;

            unitModule = unitModuleBase as T;
            return true;
        }
        
        public void Dispose()
        {
        }
    }
}