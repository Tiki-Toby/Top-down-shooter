using GameLogic.AnimationLogic;
using UnityEngine;

namespace GameLogic.LookDirectionLogic
{
    public class InputLookDirectionController : ALookDirectionController
    {
        public InputLookDirectionController(ViewController viewController) : base(viewController)
        {
        }
        
        public override void UpdateLookDirection()
        {
            Vector2 playerPosition = ViewController.UnitView.transform.position;
            Vector2 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            SetLookDirection((pointerPosition - playerPosition).normalized);
        }
    }
}