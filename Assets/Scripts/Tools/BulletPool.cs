using System;
using System.Collections;
using System.Collections.Generic;
using GameLogic.AttackLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools
{
    public class BulletPool : IDisposable, IEnumerable<BulletView>
    {
        private readonly Transform _parent;
        private readonly BulletView _bulletPrefab;
        private readonly LinkedList<BulletView> _activeBullets;
        private readonly Stack<BulletView> _pooledBullets;

        public BulletPool(BulletView bulletPrefab)
        {
            _parent = new GameObject(bulletPrefab.name + "s").transform;
            _parent.position = Vector3.zero;
            
            _bulletPrefab = bulletPrefab;
            _activeBullets = new LinkedList<BulletView>();
            _pooledBullets = new Stack<BulletView>();
        }

        public void SpawnBullet(Vector3 position, Vector2 velocity, float damage, float lifeTime)
        {
            if (_pooledBullets.TryPop(out var bulletView))
            {
                bulletView.gameObject.SetActive(true);
                bulletView.transform.position = position;
            }
            else
            {
                bulletView = Object.Instantiate(_bulletPrefab, position, Quaternion.identity, _parent);
            }
            
            _activeBullets.AddFirst(bulletView);
            float angle = Vector2.Angle(Vector2.right, velocity);
            bulletView.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            bulletView.Rigidbody.velocity = velocity;
            bulletView.Init(damage, lifeTime);
        }

        public void RemoveActiveBulletController(BulletView bulletView)
        {
            bulletView.gameObject.SetActive(false);
            _activeBullets.Remove(bulletView);
            _pooledBullets.Push(bulletView);
        }

        public void Update()
        {
            for(LinkedListNode<BulletView> node = _activeBullets.First, nextNode; node != null; node = nextNode)
            {
                nextNode = node.Next;
                if (!node.Value.IsAlive)
                {
                    node.Value.gameObject.SetActive(false);
                    _pooledBullets.Push(node.Value);
                    if (node.Previous != null) node.Previous.List.Remove(node);
                    else _activeBullets.RemoveFirst();
                }
            }
        }
        
        public void Dispose()
        {
            foreach (var pooledBullet in _pooledBullets)
                GameObject.Destroy(pooledBullet.gameObject);
            
            foreach (var activeBullet in _activeBullets)
                GameObject.Destroy(activeBullet.gameObject);
            
            _pooledBullets.Clear();
            _activeBullets.Clear();
        }

        public IEnumerator<BulletView> GetEnumerator()
        {
            return _activeBullets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _activeBullets.GetEnumerator();
        }
    }
}