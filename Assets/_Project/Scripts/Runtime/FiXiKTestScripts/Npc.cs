using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class Npc : MonoBehaviour
    {
        [Header("Interval between direction changes (seconds)")]
        [SerializeField] private float _minChangeInterval = 1f;
        [SerializeField] private float _maxChangeInterval = 5f;

        [Header("Real-time direction drift (degrees per second)")]
        [SerializeField] private float _minDriftSpeed = 10f;
        [SerializeField] private float _maxDriftSpeed = 60f;

        [Header("Movement speed range")]
        [SerializeField] private float _minSpeed = 2f;
        [SerializeField] private float _maxSpeed = 7f;

        private Character _character;
        private Vector2 _desiredDirection;
        private float _timeUntilNextChange;

        public void Init()
        {
            _character = GetComponent<Character>();
            _character.Init();

            SetRandomDirection();
            ScheduleNextChange();
            SetRandomSpeed();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            DriftDesiredDirection(deltaTime);
            _character.Move(transform.right);
            _character.Rotate(_desiredDirection, deltaTime);

            _timeUntilNextChange -= deltaTime;

            if (_timeUntilNextChange <= 0f)
            {
                SetRandomDirection();
                ScheduleNextChange();
                SetRandomSpeed();
            }
        }

        private void SetRandomDirection()
        {
            float randomAngle = Random.Range(0f, 360f);
            float rad = randomAngle * Mathf.Deg2Rad;
            _desiredDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        }

        private void ScheduleNextChange() =>
            _timeUntilNextChange = Random.Range(_minChangeInterval, _maxChangeInterval);

        private void SetRandomSpeed()
        {
            float randomSpeed = Random.Range(_minSpeed, _maxSpeed);
            _character.SetSpeed(randomSpeed);
        }

        private void DriftDesiredDirection(float deltaTime)
        {
            float driftSpeed = Random.Range(_minDriftSpeed, _maxDriftSpeed);
            float sign = Random.value > 0.5f ? 1f : -1f;
            float angleDelta = driftSpeed * sign * deltaTime;

            float currentAngle = Mathf.Atan2(_desiredDirection.y, _desiredDirection.x) * Mathf.Rad2Deg;
            float newAngle = currentAngle + angleDelta;
            float rad = newAngle * Mathf.Deg2Rad;
            _desiredDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        }

        private void OnDrawGizmosSelected()
        {
            if (_character != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, _desiredDirection);
            }
        }
    }
}