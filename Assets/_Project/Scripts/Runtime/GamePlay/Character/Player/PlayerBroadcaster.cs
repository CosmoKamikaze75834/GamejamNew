using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class PlayerBroadcaster : MonoBehaviour, IAttacker, IEntity
    {
        [SerializeField] private float _originalSpeed;
        [SerializeField] private float _keysRotationSpeed = 300f;

        private readonly List<Npc> _recruits = new();

        private Character _character;
        private Camera _camera;
        private Shooter _shooter;
        private IInputReader _inputReader;
        private Vector2? _followTarget;
        private int _recruitsCount;

        public event Action CountChanged;

        public Color Color => _character.Color;

        public int RecruitsCount => _recruitsCount;

        public Transform Transform { get; private set; }

        public LangData TeamName { get; private set; }

        public void Init(IInputReader inputReader)
        {
            _inputReader = inputReader;
            _character = GetComponent<Character>();
            _character.Init(_originalSpeed);
            Transform = transform;
            _camera = Camera.main;

            _inputReader.FollowPointPressed += OnFollowPointPressed;
            _inputReader.ShootPressed += OnShootPressed;
            _character.DestinationReached += OnDestinationReached;
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;

            if (_inputReader.PointPosition.HasValue)
            {
                Vector3 playerScreenPos = _camera.WorldToScreenPoint(_character.transform.position);
                Vector2 direction = _inputReader.PointPosition.Value - (Vector2)playerScreenPos;
                _character.Rotate(direction, deltaTime);
            }
            else if (_inputReader.RotationAim != 0)
            {
                float deltaAngle = _inputReader.RotationAim * _keysRotationSpeed * deltaTime;
                _character.RotateBy(deltaAngle, deltaTime);
            }

            if (_followTarget.HasValue)
                _character.MoveTo(_followTarget.Value, deltaTime);
            else
                _character.Move(_inputReader.Movement);
        }

        private void OnDestroy()
        {
            _inputReader.FollowPointPressed -= OnFollowPointPressed;
            _character.DestinationReached -= OnDestinationReached;
            _inputReader.ShootPressed -= OnShootPressed;
        }

        public void SetShooter(Shooter shooter) =>
            _shooter = shooter;

        public void SetTeamName(LangData langData) =>
            TeamName = langData;

        public void SetColor(Color color) =>
            _character.SetColor(color);

        public void AddRecruit(Npc npc)
        {
            if (!_recruits.Contains(npc))
            {
                _recruits.Add(npc);
                _recruitsCount++;
                CountChanged?.Invoke();
            }
        }

        public void RemoveRecruit(Npc npc)
        {
            if (_recruits.Remove(npc))
            {
                _recruitsCount--;
                CountChanged?.Invoke();
            }
        }

        private Vector2 CalculateDirection()
        {
            if (_inputReader.PointPosition.HasValue == false)
                return Vector2.zero;

            Vector2 screenPos = _inputReader.PointPosition.Value;
            Vector3 worldPos = _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));

            return (Vector2)worldPos;
        }

        private void OnFollowPointPressed() =>
            _followTarget = CalculateDirection();

        private void OnDestinationReached() =>
            _followTarget = null;

        private void OnShootPressed() =>
            _shooter.TryShoot(transform.position, transform.up);
    }
}