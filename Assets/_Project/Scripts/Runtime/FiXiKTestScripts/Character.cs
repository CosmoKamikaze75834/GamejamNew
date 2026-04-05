using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Rigidbody2D))]  
    public class Character : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private Mover _mover;
        private Rotator _rotator;
        private Color _color;

        public event Action<Color> ColorChanged;
        public event Action DestinationReached;

        public Color Color => _color;

        private void Awake()
        {
            _mover = new Mover(_rigidbody, OnDestinationReached);
            _rotator = new Rotator(transform);
        }

        public void SetColor(Color color)
        {
            _color = color;
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