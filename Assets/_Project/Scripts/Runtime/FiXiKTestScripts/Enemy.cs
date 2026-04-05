using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class Enemy : MonoBehaviour, IAttacker, IEntity
    {
        [SerializeField] private Character _character;
        [SerializeField] private Wanderer _wanderer;
        [SerializeField] private Scanner _scanner;
        [SerializeField] private float _visibleDistance = 10f;
        [SerializeField] private float _attackDistance = 5f;
        [SerializeField] private float _scanInterval = 0.5f;

        private List<IEntity> _cachedTargets = new();
        private Shooter _shooter;
        private IEntity _currentTarget;
        private float _lastScanTime;

        public Color Color => _character.Color;

        public Transform Transform { get; private set; }

        private void Awake() =>
            Transform = transform;

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            if (Time.time >= _lastScanTime + _scanInterval)
            {
                PerformFilteredScan();
                _lastScanTime = Time.time;
            }

            _currentTarget = GetNearestTargetFromCache();

            if (_currentTarget != null)
            {
                Vector2 targetPos = _currentTarget.Transform.position;
                float distanceToTarget = Vector2.Distance(Transform.position, targetPos);

                _character.RotateTo(targetPos, deltaTime);
                _character.MoveTo(targetPos, deltaTime);

                if (distanceToTarget <= _attackDistance)
                {
                    Vector2 direction = (targetPos - (Vector2)Transform.position).normalized;
                    _shooter?.TryShoot(Transform.position, direction);
                }
            }
            else
            {
                _wanderer.UpdateWander(deltaTime);
            }
        }

        private void PerformFilteredScan()
        {
            List<IEntity> allTargets = _scanner.Scan(Transform.position, _visibleDistance);

            _cachedTargets = allTargets
                .Where(target =>
                {
                    if (target == (IEntity)this)
                        return false;

                    if (target is Enemy)
                        return false;

                    if (target is Player)
                        return false;

                if (target is Npc npc && npc.Owner == (IAttacker)this)
                        return false;

                    return true;
                })
                .ToList();
        }

        private IEntity GetNearestTargetFromCache()
        {
            IEntity nearest = null;
            float minDist = float.MaxValue;
            Vector2 myPos = Transform.position;

            foreach (var target in _cachedTargets)
            {
                if (target == null || target.Transform == null)
                    continue;

                float dist = Vector2.Distance(myPos, target.Transform.position);

                if (dist < minDist && dist <= _visibleDistance)
                {
                    minDist = dist;
                    nearest = target;
                }
            }
            return nearest;
        }

        public void SetShooter(Shooter shooter) =>
            _shooter = shooter;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _visibleDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}