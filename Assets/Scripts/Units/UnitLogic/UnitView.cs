using System;
using Units.UnitDescription;
using UnityEngine;
using UnityEngine.Events;

namespace Units.UnitLogic
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Space] [Header("Unit Datas")]
        [SerializeField] private UnitDefaultStructData _unitDefaultData;
        [SerializeField] private UnitAttackStructData _unitAttackData;

        [Space] [Header("Unit Parts")] 
        [SerializeField] private Transform _handsTransform;
        [SerializeField] private Transform _bulletSpawnPoint;

        [SerializeField] private UnityEvent<float> _takeDamage;

        public Animator Animator => _animator;
        public SpriteRenderer Sprite => _sprite;
        public Rigidbody2D Rigidbody => _rigidbody;

        public UnitDefaultStructData UnitDefaultData => _unitDefaultData;
        public UnitAttackStructData UnitAttackData => _unitAttackData;
        
        public Transform HandsTransform => _handsTransform;
        public Transform BulletSpawnPoint => _bulletSpawnPoint;
        
        public UnityEvent<float> TakeDamage => _takeDamage;
    }
}