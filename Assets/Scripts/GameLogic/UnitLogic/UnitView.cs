using UnityEngine;

namespace GameLogic.UnitLogic
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;

        public Rigidbody2D Rigidbody => _rigidbody;
        public Animator Animator => _animator;
    }
}