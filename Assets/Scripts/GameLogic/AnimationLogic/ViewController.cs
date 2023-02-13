using Constants;
using GameLogic.MoveLogic;
using GameLogic.UnitLogic;
using UnityEngine;

namespace GameLogic.AnimationLogic
{
    public class ViewController
    {
        private readonly UnitView _unitView;

        private Vector3 _targetDirection; 
        //скорость поворота через конфиг который будет синглетоном

        public ViewController(UnitView unitView)
        {
            _unitView = unitView;
        }

        public void Move(IMoveController moveController)
        {
            _unitView.Rigidbody.AddForce(moveController.CurrentVelocityVector);
        }

        public void SetLookDirection(Vector3 direction)
        {
            _targetDirection = direction;
        }

        public void Attack()
        {
            
        }

        public void Update()
        {
            Vector3 currentLookDirection = _unitView.transform.forward;
            if ((_targetDirection - currentLookDirection).magnitude > ConstantsOffsetValue.TwoVectorsError)
            {
                currentLookDirection = Vector3.Lerp(currentLookDirection, _targetDirection, ConstantsVelocity.LookDirectionRotateVelocity * Time.deltaTime);
                _unitView.transform.rotation.SetLookRotation(currentLookDirection);
            }
        }
    }
}