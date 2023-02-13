using System;
using System.Collections.Generic;
using GameLogic.UnitLogic;
using GameLogic.UnitLogic.Factory;

namespace Tools
{
    public class UnitsPool : IDisposable
    {
        private readonly BaseUnitFactory _unitFactory;
        private readonly List<UnitController> _activeUnits;
        private readonly List<UnitController> _pooledUnits;

        public UnitsPool(BaseUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
            _activeUnits = new List<UnitController>();
            _pooledUnits = new List<UnitController>();
        }
        public UnitController
        
        public void Dispose()
        {
        }
    }
}