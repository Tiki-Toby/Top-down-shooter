using System;

namespace Units.UnitLogic
{
    public class UnitEventController
    {
        private readonly UnitController _unitController;
        
        private Action _unitDead; // kill action
        private Action _unitAfterDead; // destroy action (after animation etc.)
        private Action<float> _unitTakeDamage;

        public UnitEventController(UnitController unitController)
        {
            _unitController = unitController;
        }

        public void OnUnitDeadSubscribe(Action invoke) => _unitDead += invoke; 
        public void OnUnitAfterDeadSubscribe(Action invoke) => _unitAfterDead += invoke; 
        public void OnUnitTakeDamageSubscribe(Action<float> invoke) => _unitTakeDamage += invoke; 

        public void TakeDamage(float damage) => _unitTakeDamage?.Invoke(damage);
        public void KillUnit() => _unitDead?.Invoke();
        public void BeforeDestroy() => _unitAfterDead?.Invoke();

        public void Reset()
        {
            _unitDead = null;
            _unitAfterDead = null;
            _unitTakeDamage = null;
        }
    }
}