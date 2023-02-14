using Constants;
using GameLogic.MoveLogic;
using GameLogic.UnitLogic;
using UnityEngine;

namespace GameLogic.AnimationLogic
{
    public class ViewController
    {
        private readonly UnitView _unitView;

        public UnitView UnitView => _unitView;
        
        public ViewController(UnitView unitView)
        {
            _unitView = unitView;
        }

        public void Move(IMoveController moveController)
        {
            _unitView.Rigidbody.AddForce(moveController.CurrentVelocityVector);
        }

        public void SetLookDirection(Vector2 direction)
        {
            float angle = Vector2.Angle(Vector2.right, direction) * Mathf.Sign(direction.y);
            _unitView.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        public void Attack()
        {
            
        }

        public void Update()
        {
        }

        public void SetActive(bool isActive)
        {
            _unitView.gameObject.SetActive(isActive);
        }

        public void SetPosition(Vector3 position)
        {
            _unitView.transform.position = position;
        }
    }
}