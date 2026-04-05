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
        [SerializeField] private float _fleeSpeedMultiplier = 1.5f;

        private readonly List<IEntity> _cachedTargets = new();
        private readonly List<IAttacker> _cachedAttackers = new();
        private readonly List<Npc> _recruits = new();
        private float _originalSpeed;
        private Shooter _shooter;
        private IEntity _currentTarget;
        private float _lastScanTime;
        private IAttacker _threatToFlee;
        private int _recruitsCount;

        public Color Color => _character.Color;

        public Transform Transform { get; private set; }

        public int RecruitsCount => _recruitsCount;

        private void Awake() =>
            Transform = transform;

        private void Start() =>
            _originalSpeed = _character.Speed;

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            if (Time.time >= _lastScanTime + _scanInterval)
            {
                PerformFilteredScan();
                EvaluateThreat();
                _lastScanTime = Time.time;
            }

            if (_threatToFlee != null && _threatToFlee.Transform != null)
            {
                Vector2 threatPos = _threatToFlee.Transform.position;
                Vector2 away = ((Vector2)Transform.position - threatPos).normalized;
                _character.Move(away);
                _character.Rotate(away, deltaTime);
                _character.SetSpeed(_originalSpeed * _fleeSpeedMultiplier);

                IEntity shootTarget = GetNearestEnemyNpcFromCache();

                if (shootTarget != null && Vector2.Distance(Transform.position, shootTarget.Transform.position) <= _attackDistance)
                {
                    Vector2 shootDir = ((Vector2)shootTarget.Transform.position - (Vector2)Transform.position).normalized;
                    _character.Rotate(shootDir, deltaTime);
                    _shooter?.TryShoot(Transform.position, shootDir);
                }
            }
            else
            {
                _character.SetSpeed(_originalSpeed);
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
        }

        private void PerformFilteredScan()
        {
            List<IEntity> allTargets = _scanner.Scan(Transform.position, _visibleDistance);

            _cachedTargets.Clear();
            _cachedAttackers.Clear();

            foreach (var entity in allTargets)
            {
                // пропускаем себя
                if (entity == (IEntity)this)
                    continue;

                // если это IAttacker (другой враг или игрок) – добавляем в _cachedAttackers
                if (entity is IAttacker attacker && attacker != (IAttacker)this)
                {
                    _cachedAttackers.Add(attacker);
                    continue;
                }

                // если это NPC, которого не владеем – добавляем в _cachedTargets для атаки
                if (entity is Npc npc && npc.Owner != (IAttacker)this)
                {
                    _cachedTargets.Add(npc);
                }
            }
        }

        // CHANGE: оценка угрозы теперь только по _cachedAttackers, без FindObjectsOfType
        private void EvaluateThreat()
        {
            _threatToFlee = null;

            // отбираем тех, у кого армия больше или равна (сильный противник)
            var threats = _cachedAttackers
                .Where(a => a.RecruitsCount >= RecruitsCount)
                .ToList();

            if (threats.Count == 0)
                return;

            // из них выбираем ближайшего
            float minDist = float.MaxValue;
            Vector2 myPos = Transform.position;

            foreach (var threat in threats)
            {
                if (threat.Transform == null) continue;
                float dist = Vector2.Distance(myPos, threat.Transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    _threatToFlee = threat;
                }
            }
        }

        private IEntity GetNearestEnemyNpcFromCache()
        {
            IEntity nearest = null;
            float minDist = float.MaxValue;
            Vector2 myPos = Transform.position;

            foreach (var target in _cachedTargets)
            {
                if (target == null || target.Transform == null) 
                    continue;

                float dist = Vector2.Distance(myPos, target.Transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = target;
                }
            }

            return nearest;
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

        public void AddRecruit(Npc npc)
        {
            if (!_recruits.Contains(npc))
            {
                _recruits.Add(npc);
                _recruitsCount++;
            }
        }

        public void RemoveRecruit(Npc npc)
        {
            if (_recruits.Remove(npc))
                _recruitsCount--;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _visibleDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}