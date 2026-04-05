using UnityEngine;

namespace FiXiKTestScripts
{
    public class Wanderer : MonoBehaviour
    {
        [SerializeField] private Character _character;

        [Header("Interval between direction changes (seconds)")]
        [SerializeField] private float _minChangeInterval = 1f;
        [SerializeField] private float _maxChangeInterval = 5f;

        [Header("Real-time direction drift (degrees per second)")]
        [SerializeField] private float _minDriftSpeed = 10f;
        [SerializeField] private float _maxDriftSpeed = 60f;

        [Header("Movement speed range")]
        [SerializeField] private float _minSpeed = 2f;
        [SerializeField] private float _maxSpeed = 7f;

        private Vector2 _desiredDirection;
        private float _timeUntilNextChange;

        private WandererStats _stats;

        private void Awake()
        {
            SetRandomDirection();
            ScheduleNextChange();
            SetRandomSpeed();
        }

        public void SetStats(WandererStats stats) =>
            _stats = stats;

        public void UpdateWander(float deltaTime)
        {
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
            float randomAngle = UnityEngine.Random.Range(0f, 360f);
            float rad = randomAngle * Mathf.Deg2Rad;
            _desiredDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        }

        private void ScheduleNextChange() =>
            _timeUntilNextChange = UnityEngine.Random.Range(_minChangeInterval, _maxChangeInterval);

        private void SetRandomSpeed()
        {
            float randomSpeed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);
            _character.SetSpeed(randomSpeed);
        }

        private void DriftDesiredDirection(float deltaTime)
        {
            float driftSpeed = UnityEngine.Random.Range(_minDriftSpeed, _maxDriftSpeed);
            float sign = UnityEngine.Random.value > 0.5f ? 1f : -1f;
            float angleDelta = driftSpeed * sign * deltaTime;

            float currentAngle = Mathf.Atan2(_desiredDirection.y, _desiredDirection.x) * Mathf.Rad2Deg;
            float newAngle = currentAngle + angleDelta;
            float rad = newAngle * Mathf.Deg2Rad;
            _desiredDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        }

        private void OnDrawGizmosSelected()
        {
            if (_character == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _desiredDirection);
        }
    }
}