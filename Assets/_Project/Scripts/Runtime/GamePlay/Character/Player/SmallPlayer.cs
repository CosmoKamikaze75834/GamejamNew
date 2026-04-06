using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Npc))]
    [RequireComponent(typeof(Collider2D))]
    public class SmallPlayer : MonoBehaviour, IEntity
    {
        [SerializeField] private float _originalSpeed;
        [SerializeField] private float _keysRotationSpeed = 300f;

        private Character _character;
        private Npc _npc;
        private Camera _camera;
        private IInputReader _inputReader;
        private Vector2? _followTarget;

        public event Action<SmallPlayer> Required;

        public Transform Transform { get; private set; }

        public Color Color => _character.Color;

        public void Init(
            IInputReader inputReader, 
            WandererStats wandererStats, 
            FleeBehaviourStats fleeStats)
        {
            _npc = GetComponent<Npc>();
            _npc.Init(wandererStats, fleeStats);
            _npc.enabled = false;
            _inputReader = inputReader;
            _character = GetComponent<Character>();
            _character.Init(_originalSpeed);
            _camera = Camera.main;
            Transform = transform;

            _inputReader.FollowPointPressed += OnFollowPointPressed;
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

        private void OnDestroy() =>
            Unsubscribe();

        public Npc ConvertToNpc()
        {
            GetComponent<Rigidbody2D>().mass = 1;
            Unsubscribe();
            enabled = false;
            _npc.enabled = true;

            Required?.Invoke(this);

            return _npc;
        }

        public void Destroy() =>
            Destroy(gameObject);

        public void SetColor(Color color) =>
            _character.SetColor(color);

        private void Unsubscribe()
        {
            _inputReader.FollowPointPressed -= OnFollowPointPressed;
            _character.DestinationReached -= OnDestinationReached;
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
    }
}