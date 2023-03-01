using UnityEngine;

namespace Units.AttackLogic
{
    public class InputAttackController : SimpleAttackController
    {
        public InputAttackController(BulletManager bulletManager) : base(bulletManager)
        {
        }

        protected override bool IsAdditionalAttackConditionHandle()
        {
            return Input.GetMouseButton(0);
        }
    }
}