using System;
using System.Collections;
using System.Collections.Generic;
using GameLogic.UnitLogic;
using GameLogic.UnitLogic.Factory;
using UnityEngine;

namespace Tools
{
    public class UnitsPool : IDisposable, IEnumerable<UnitController>
    {
        private readonly BaseUnitFactory _unitFactory;
        private readonly LinkedList<UnitController> _activeUnits;
        private readonly Stack<UnitController> _pooledUnits;

        public UnitController FirstActive => _activeUnits.Count > 0 ? _activeUnits.First.Value : GetNextController(Vector3.zero);
        public int ActiveCount => _activeUnits.Count;
        public int PooledCount => _pooledUnits.Count;

        public UnitsPool(BaseUnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
            _activeUnits = new LinkedList<UnitController>();
            _pooledUnits = new Stack<UnitController>();
        }

        public UnitController GetNextController(Vector3 position)
        {
            if (_pooledUnits.TryPeek(out var unitController))
            {
                unitController.ViewController.SetActive(true);
                unitController.ViewController.SetPosition(position);
            }
            else
            {
                unitController = _unitFactory.CreateUnitController(position);
                _activeUnits.AddFirst(unitController);
            }

            return unitController;
        }

        public void RemoveActiveUnitController(UnitController unitController)
        {
            unitController.ViewController.SetActive(false);
            _activeUnits.Remove(unitController);
            _pooledUnits.Push(unitController);
        }
        
        public void Dispose()
        {
        }

        public IEnumerator<UnitController> GetEnumerator()
        {
            return _activeUnits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _activeUnits.GetEnumerator();
        }
    }
}