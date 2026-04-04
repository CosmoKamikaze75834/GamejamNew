using UnityEngine;
using VContainer;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class Player : MonoBehaviour
    {
        private Camera _camera;
        private Character _character;
        private IInputReader _inputReader;

        private Vector2? _followTarget;

        [Inject]
        public void Construct(IInputReader inputReader)
        {
            _inputReader = inputReader;
            _camera = Camera.main;
        }

        public void Init()
        {
            _character = GetComponent<Character>();
            _character.Init();

            _inputReader.FollowPointPressed += OnFollowPointPressed;
            _character.DestinationReached += OnDestinationReached;
        }

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
        }

        private void OnFollowPointPressed()
        {
            Debug.Log("ddfdfsfdsfd");
            Vector2 screenPos = _inputReader.PointPosition;
            Vector3 worldPos = _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
            _followTarget = (Vector2)worldPos;
        }

        private void OnDestinationReached() =>
            _followTarget = null;
    }
}