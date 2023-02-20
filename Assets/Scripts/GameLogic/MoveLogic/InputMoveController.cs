using UnityEngine;

namespace GameLogic.MoveLogic
{
    public class InputMoveController : BaseMoveController
    {
        public InputMoveController() : base()
        {
        } 
        
        protected override Vector2 UpdateDirection()
        {
            float yInputAxis = Input.GetAxis("Vertical");
            float xInputAxis = Input.GetAxis("Horizontal");
            Vector2 direction = new Vector2(xInputAxis, yInputAxis);
            return Vector2.ClampMagnitude(direction, 1f);
        }
    }
}