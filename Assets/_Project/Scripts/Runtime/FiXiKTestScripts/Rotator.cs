using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class Rotator
    {
        private readonly Transform _transform;
        private readonly float _speed = 300f;

        public Rotator(Transform transform)
        {
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
        }

        public void Rotate(Vector2 direction, float deltaTime)
        {
            if (direction == Vector2.zero)
                return;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, _speed * deltaTime);
        }

        public void RotateTo(Vector2 worldPoint, float deltaTime)
        {
            Vector2 direction = worldPoint - (Vector2)_transform.position;
            Rotate(direction, deltaTime);
        }
    }
}