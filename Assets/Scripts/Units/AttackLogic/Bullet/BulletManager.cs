using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Units.AttackLogic
{
    public class BulletManager
    {
        private readonly Dictionary<BulletView, BulletPool> _bulletPools;

        public BulletManager()
        {
            _bulletPools = new Dictionary<BulletView, BulletPool>();
        }

        public void SpawnBullet(BulletView bulletPrefab, Vector3 position, Vector2 force, float damage, float lifeTime)
        {
            if (!_bulletPools.TryGetValue(bulletPrefab, out BulletPool bulletPool))
            {
                bulletPool = new BulletPool(bulletPrefab);
                _bulletPools.Add(bulletPrefab, bulletPool);
            }

            bulletPool.SpawnBullet(position, force, damage, lifeTime);
        }

        public void Update()
        {
            foreach (var bulletPool in _bulletPools.Values)
            {
                bulletPool.Update();
            }
        }
    }
}