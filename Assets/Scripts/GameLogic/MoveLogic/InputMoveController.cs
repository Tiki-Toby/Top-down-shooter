using Constants;
using UnityEngine;

namespace GameLogic.MoveLogic
{
    public class InputMoveController : IMoveController
    {
        private Vector2 _currentVelocity;
        
        public float CurrentVelocity => _currentVelocity.magnitude;
        public Vector2 MoveDirection => _currentVelocity.normalized;
        public Vector2 CurrentVelocityVector => _currentVelocity;
        public bool IsMovement => CurrentVelocity == 0;

        public float MaxVelocity { get; }

        public InputMoveController(float maxVelocity)
        {
            MaxVelocity = maxVelocity;
            _currentVelocity = Vector2.zero;
        }
        
        public void Move()
        {
            float yInputAxis = Input.GetAxis("Vertical");
            float xInputAxis = Input.GetAxis("Horizontal");
            Vector2 direction = new Vector2(xInputAxis, yInputAxis);
            direction = Vector2.ClampMagnitude(direction, 1f);

            _currentVelocity = direction * MaxVelocity;
        }
    }
}