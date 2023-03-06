using System;
using Units.UnitLogic;
using UnityEngine;

namespace Units.AttackLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _defaultVelocity;
        private float _damage;
        private bool _isAlive;

        public bool IsAlive => _lifeTime >= Time.time && _isAlive;
        public float LifeTime => _lifeTime;
        public float DefaultVelocity => _defaultVelocity;
        public Rigidbody2D Rigidbody => _rigidbody;

        public void Init(float damage, float lifeTime)
        {
            _damage = damage;
            _lifeTime = Time.time + lifeTime;
            _isAlive = true;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var unitView = other.GetComponent<UnitView>();
            if (unitView != null)
            {
                unitView.TakeDamage.Invoke(_damage);
            }

            _isAlive = false;
        }
    }
}