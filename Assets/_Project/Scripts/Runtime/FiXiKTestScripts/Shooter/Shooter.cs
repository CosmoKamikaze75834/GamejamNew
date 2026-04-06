using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class Shooter
    {
        private readonly IAttacker _attacker;
        private readonly Bullet _bulletPrefab;

        private float _lastShootTime = -Mathf.Infinity;
        private float _reloadTime;

        public Shooter(IAttacker attaker, Bullet bulletPrefab, float reloadTime)
        {
            _attacker = attaker;
            _bulletPrefab = bulletPrefab;

            if (reloadTime <= 0)
                throw new ArgumentOutOfRangeException(nameof(reloadTime), reloadTime, "Значение должно быть больше нуля");

            _reloadTime = reloadTime;
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