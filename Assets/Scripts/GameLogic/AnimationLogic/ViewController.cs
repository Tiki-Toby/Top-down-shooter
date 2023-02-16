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
        public Vector3 BulletSpawnPosition => _unitView.BulletSpawnPoint.position;
        
        public ViewController(UnitView unitView)
        {
            _unitView = unitView;
        }

        public void Move(IMoveController moveController)
        {
            _unitView.Rigidbody.AddForce(moveController.CurrentVelocityVector);
            _unitView.Animator.SetFloat(ConstantsAnimationParameters.VelocityRatio, moveController.CurrentVelocity / moveController.MaxVelocity);
        }

        public void SetLookDirection(Vector2 direction)
        {
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