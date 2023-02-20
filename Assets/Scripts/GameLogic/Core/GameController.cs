using System;
using GameLogic.AttackLogic;
using GameLogic.UnitLogic;
using UnityEngine;
using Zenject;

namespace GameLogic.Core
{
    public class GameController : MonoBehaviour
    {
        private UnitManager _unitManager;
        private BulletManager _bulletManager;
        private LocationManager _locationManager;
        
        [Inject]
        public void Construct(UnitManager unitManager, 
            BulletManager bulletManager,
            LocationManager locationManager)
        {
            _unitManager = unitManager;
            _bulletManager = bulletManager;
            _locationManager = locationManager;
        }

        private void Start()
        {
            _locationManager.LoadLocation("TestLocation");
        }

        private void Update()
        {
            if (_unitManager.CharacterUnit == null || !_unitManager.CharacterUnit.UnitDataController.IsAlive)
            {
                //reload level or show window about death
            }
            else
            {
                _bulletManager.Update();
                _unitManager.Update();
            }
        }
    }
}