using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class Shooter
    {
        private readonly IAttacker _attacker;
        private readonly Bullet _bulletPrefab;

        private float _lastShootTime = -Mathf.Infinity;
        private float _reloadTime = 0.5f;

        public Shooter(IAttacker attaker, Bullet bulletPrefab)
        {
            _attacker = attaker;
            _bulletPrefab = bulletPrefab;
        }

        public void SetReloadTime(float time)
        {
            if(time <= 0)
                throw new ArgumentOutOfRangeException(nameof(time), time, "Должно быть больше нуля");

            _reloadTime = time;
        }

        public bool TryShoot(Vector2 startPosition, Vector2 direction)
        {
            if (Time.time < _lastShootTime + _reloadTime)
                return false;

            _lastShootTime = Time.time;
            Bullet bullet = UnityEngine.Object.Instantiate(_bulletPrefab, startPosition, Quaternion.identity);
            bullet.Init(_attacker, direction);

            return true;
        }
    }
}