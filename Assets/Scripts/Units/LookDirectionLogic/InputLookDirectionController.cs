using UnityEngine;

namespace Units.LookDirectionLogic
{
    public class InputLookDirectionController : BaseLookDirectionController
    {
        public override void Invoke()
        {
            Vector2 playerPosition = UnitController.ViewController.UnitPosition; 
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            SetLookDirection((worldPosition - playerPosition).normalized);
        }
    }
}