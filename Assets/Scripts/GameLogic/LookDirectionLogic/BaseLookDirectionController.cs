using GameLogic.AnimationLogic;
using GameLogic.UnitLogic;
using UnityEngine;

namespace GameLogic.LookDirectionLogic
{
    public abstract class BaseLookDirectionController : BaseUnitModuleController
    {
        protected readonly ViewController ViewController;
        private Vector2 _lookDirection;
        
        public Vector2 LookDirection => _lookDirection;

        protected BaseLookDirectionController(ViewController viewController)
        {
            ViewController = viewController;
        }
        
        public abstract void UpdateLookDirection();

        protected void SetLookDirection(Vector2 direction)
        {
            if(_lookDirection == direction)
                return;

            _lookDirection = direction;
            ViewController.SetLookDirection(_lookDirection);
        }
    }
}