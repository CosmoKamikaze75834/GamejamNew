using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Rigidbody2D))]  
    public class Character : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Mover _mover;
        private Rotator _rotator;

        public event Action<Color> ColorChanged;
        public event Action DestinationReached;

        public Color Color { get; private set; }

        public float Speed => _mover.Speed;

        public void Init(float speed)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _mover = new Mover(_rigidbody, speed, OnDestinationReached);
            _rotator = new Rotator(transform);
        }

        public void SetColor(Color color)
        {
            Color = color;
            ColorChanged?.Invoke(color);
        }

        public void SetSpeed(float speed) =>
            _mover.SetSpeed(speed);

        public void Move(Vector2 direction) =>
            _mover.Move(direction);

        public void MoveTo(Vector2 position, float deltaTime) =>
            _mover.MoveTo(position, deltaTime);

        public void Rotate(Vector2 direction, float deltaTime) =>
            _rotator.Rotate(direction, deltaTime);

        public void RotateTo(Vector2 target, float deltaTime) =>
            _rotator.Rotate(target, deltaTime);

        private void OnDestinationReached() =>
            DestinationReached?.Invoke();
    }
}