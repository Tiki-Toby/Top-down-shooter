using Constants;
using Units.MoveLogic;
using Units.UnitLogic;
using UnityEngine;

namespace Units.AnimationLogic
{
    public class ViewController
    {
        private readonly UnitView _unitView;

        private Vector2 _targetLookDirection;

        public UnitView UnitView => _unitView;
        public Vector2 UnitPosition => _unitView.transform.position;
        public Vector2 UnitLookDirection => _targetLookDirection;
        public Vector3 BulletSpawnPosition => _unitView.BulletSpawnPoint.position;
        
        public ViewController(UnitView unitView)
        {
            _unitView = unitView;
        }

        public void Move(BaseMoveController moveController)
        {
            _unitView.Rigidbody.AddForce(moveController.CurrentVelocityVector);
            float velocityRatio = moveController.CurrentVelocity / moveController.UnitController.UnitDataController.MaxVelocity;
            _unitView.Animator.SetFloat(ConstantsAnimationParameters.VelocityRatio, velocityRatio);
        }

        public void SetLookDirection(Vector2 direction)
        {
            _targetLookDirection = direction;
            Vector3 scale = _unitView.HandsTransform.localScale;
            float angle = Vector2.Angle(Vector2.right, direction) * Mathf.Sign(direction.y);
            
            float factor = direction.x > 0 ? 1 : -1;
            _unitView.Sprite.flipX = factor < 0;
            scale.y = Mathf.Abs(scale.y) * factor;
            _unitView.HandsTransform.localScale = scale;
            
            _unitView.HandsTransform.rotation = Quaternion.Euler(Vector3.forward * angle);
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