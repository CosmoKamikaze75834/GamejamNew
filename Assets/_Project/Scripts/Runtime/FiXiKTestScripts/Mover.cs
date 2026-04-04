using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class Mover
    {
        private static readonly float Threshold = 0.2f;

        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;
        private readonly Action _destinationReached;
        private float _speed = 10f;

        public Mover(Rigidbody2D rigidbody, Action destinationReached)
        {
            _rigidbody = rigidbody != null ? rigidbody : throw new ArgumentNullException(nameof(rigidbody));

            _transform = _rigidbody.transform;
            _destinationReached = destinationReached;
        }

        public void SetSpeed(float speed)
        {
            if (speed < 0)
                throw new ArgumentOutOfRangeException(nameof(speed), speed, "Значение должно быть положительным");

            _speed = speed;
        }


        public void Move(Vector2 direction) =>
            _rigidbody.linearVelocity = direction * _speed;

        public void MoveTo(Vector2 position, float deltaTime)
        {
            if(Vector2.Distance(position, _transform.position) < Threshold)
            {
                _destinationReached?.Invoke();

                return;
            }

            Vector2 direction = (position - _rigidbody.position).normalized;
            Vector2 newPosition = _rigidbody.position + _speed * deltaTime * direction;
            _rigidbody.MovePosition(newPosition);
        }
    }
}