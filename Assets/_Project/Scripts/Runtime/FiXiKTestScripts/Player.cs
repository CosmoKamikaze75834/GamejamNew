using UnityEngine;
using VContainer;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class Player : MonoBehaviour, IAttacker, IEntity
    {
        [SerializeField] private Character _character;

        private Camera _camera;
        private Shooter _shooter;
        private IInputReader _inputReader;

        private Vector2? _followTarget;

        public Color Color => _character.Color;

        public Transform Transform { get; private set; }

        [Inject]
        public void Construct(IInputReader inputReader)
        {
            _inputReader = inputReader;
            _camera = Camera.main;

            _inputReader.FollowPointPressed += OnFollowPointPressed;
            _inputReader.ShootPressed += OnShootPressed;
            _character.DestinationReached += OnDestinationReached;
        }

        private void Awake() =>
            Transform = transform;

        private void Update()
        {
            Vector3 playerScreenPos = _camera.WorldToScreenPoint(_character.transform.position);
            Vector2 mouseScreenPos = _inputReader.PointPosition;
            Vector2 direction = mouseScreenPos - (Vector2)playerScreenPos;

            _character.Rotate(direction, Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (_followTarget.HasValue)
                _character.MoveTo(_followTarget.Value, Time.fixedDeltaTime);
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

        private Vector2 CalculateDirection()
        {
            Vector2 screenPos = _inputReader.PointPosition;
            Vector3 worldPos = _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));

            return (Vector2)worldPos;
        }

        private void OnFollowPointPressed() =>
            _followTarget = CalculateDirection();

        private void OnDestinationReached() =>
            _followTarget = null;

        private void OnShootPressed()
        {
            Vector2 worldMouse = CalculateDirection();
            Vector2 direction = worldMouse - (Vector2)transform.position;
            _shooter.TryShoot(transform.position, direction);
        }
    }
}