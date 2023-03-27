using BuffLogic;
using GameLogic.Core;
using Units.AttackLogic;
using Units.UnitLogic;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        private UnitManager _unitManager;
        private BuffManager _buffManager;
        private BulletManager _bulletManager;
        private LocationManager _locationManager;
        
        [Inject]
        public void Construct(UnitManager unitManager, 
            BuffManager buffManager,
            BulletManager bulletManager,
            LocationManager locationManager)
        {
            _unitManager = unitManager;
            _buffManager = buffManager;
            _bulletManager = bulletManager;
            _locationManager = locationManager;
        }

        private void Start()
        {
            _locationManager.LoadLocation("TestLocation");
        }

        private void Update()
        {
            #region TestRegion
#if true
           
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PoisonBuff buff = new PoisonBuff(20, 1f);
                _buffManager.AddBuff(_unitManager.CharacterUnit, buff);
            }
           
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MultiFloatValueBuff buff = new MultiFloatValueBuff(100f);
                TimerBuffCondition timerBuffCondition = new TimerBuffCondition(10f);
                buff.AddCondition(timerBuffCondition);
                _buffManager.AddBuff(_unitManager.CharacterUnit.UnitDataController.MaxVelocity, buff);
            }

#endif
            #endregion
            
            if (_unitManager.CharacterUnit == null)
            {
                //reload level or show window about death
                _locationManager.RespawnPlayer();
            }
            else
            {
                _bulletManager.Update();
                _unitManager.Update();
                _buffManager.Update();
                _locationManager.Update();
            }
        }
    }
}