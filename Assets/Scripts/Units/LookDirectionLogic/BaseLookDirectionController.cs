using Units.UnitLogic;
using UnityEngine;

namespace Units.LookDirectionLogic
{
    public abstract class BaseLookDirectionController : BaseUnitModuleController
    {
        private Vector2 _lookDirection;
        public Vector2 LookDirection => _lookDirection;

        protected void SetLookDirection(Vector2 direction)
        {
            if(_lookDirection == direction)
                return;

            _lookDirection = direction;
            UnitController.ViewController.SetLookDirection(_lookDirection);
        }
    }
}