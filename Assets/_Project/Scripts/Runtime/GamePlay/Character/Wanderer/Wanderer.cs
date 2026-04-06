using UnityEngine;

namespace FiXiKTestScripts
{
    public class Wanderer
    {
        private readonly Character _character;
        private readonly Transform _transform;

        private Vector2 _desiredDirection;
        private float _timeUntilNextChange;

        private readonly WandererStats _stats;

        public Wanderer(Character character, WandererStats stats)
        {
            _character = character;
            _transform = character.transform;
            _stats = stats;
        }
            
        public void UpdateWander(float deltaTime)
        {
            DriftDesiredDirection(deltaTime);
            _character.Move(_transform.right);
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
            _timeUntilNextChange = UnityEngine.Random.Range(_stats.MinChangeInterval, _stats.MaxChangeInterval);

        private void SetRandomSpeed()
        {
            float randomSpeed = UnityEngine.Random.Range(_stats.MinSpeed, _stats.MaxSpeed);
            _character.SetSpeed(randomSpeed);
        }

        private void DriftDesiredDirection(float deltaTime)
        {
            float driftSpeed = UnityEngine.Random.Range(_stats.MinDriftSpeed, _stats.MaxDriftSpeed);
            float sign = UnityEngine.Random.value > 0.5f ? 1f : -1f;
            float angleDelta = driftSpeed * sign * deltaTime;

            float currentAngle = Mathf.Atan2(_desiredDirection.y, _desiredDirection.x) * Mathf.Rad2Deg;
            float newAngle = currentAngle + angleDelta;
            float rad = newAngle * Mathf.Deg2Rad;
            _desiredDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        }
    }
}