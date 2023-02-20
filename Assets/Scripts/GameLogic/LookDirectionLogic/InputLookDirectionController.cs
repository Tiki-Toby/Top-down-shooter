using GameLogic.AnimationLogic;
using UnityEngine;

namespace GameLogic.LookDirectionLogic
{
    public class InputLookDirectionController : BaseLookDirectionController
    {
        public InputLookDirectionController(ViewController viewController) : base(viewController)
        {
        }
        
        public override void UpdateLookDirection()
        {
            Vector2 playerPosition = ViewController.UnitView.transform.position; 
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            SetLookDirection((worldPosition - playerPosition).normalized);
        }
    }
}