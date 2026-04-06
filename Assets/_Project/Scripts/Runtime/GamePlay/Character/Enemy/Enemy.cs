using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class Enemy : MonoBehaviour, IAttacker, IEntity
    {
        private readonly List<IEntity> _cachedTargets = new();
        private readonly List<IAttacker> _cachedAttackers = new();
        private readonly List<Npc> _recruits = new();

        private Character _character;
        private EnemyStats _stats;
        private WandererStats _wandererStats;
        private Wanderer _wanderer;
        private Shooter _shooter;
        private IEntity _currentTarget;
        private IAttacker _threatToFlee;
        private float _originalSpeed;
        private float _lastScanTime;
        private int _recruitsCount;

        public event Action CountChanged;

        public Color Color => _character.Color;

        public Transform Transform { get; private set; }

        public int RecruitsCount => _recruitsCount;

        public LangData TeamName { get; private set; }

        public void Init(EnemyStats stats, WandererStats wandererStats)
        {
            _stats = stats;
            _wandererStats = wandererStats;
            _character = GetComponent<Character>();
            _character.Init(_stats.MovementSpeed);
            Transform = transform;
            _wanderer = new(_character, _wandererStats);
            _originalSpeed = _character.Speed;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            if (Time.time >= _lastScanTime + _stats.ScanInterval)
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
                _character.SetSpeed(_originalSpeed * _stats.FleeSpeedMultiplier);

                IEntity shootTarget = GetNearestEnemyNpcFromCache();

                if (shootTarget != null && Vector2.Distance(Transform.position, shootTarget.Transform.position) <= _stats.AttackDistance)
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

                    if (distanceToTarget <= _stats.AttackDistance)
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

        public void SetColor(Color color) =>
            _character.SetColor(color);

        public void SetSpeed(float speed) =>
            _character.SetSpeed(speed);

        public void SetTeamName(LangData langData) =>
            TeamName = langData;

        private void PerformFilteredScan()
        {
            List<IEntity> allTargets = Scanner.Scan(Transform.position, _stats.VisibleDistance, _stats.TargetLayers);

            _cachedTargets.Clear();
            _cachedAttackers.Clear();

            foreach (IEntity entity in allTargets)
            {
                if (entity == (IEntity)this)
                    continue;

                if (entity is IAttacker attacker && attacker != (IAttacker)this)
                {
                    if (_stats.IsEnemyAllianceEnabled && attacker is Enemy)
                        continue;

                    _cachedAttackers.Add(attacker);

                    continue;
                }

                if ((entity is Npc npc) && npc.Owner != (IAttacker)this)
                {
                    if (_stats.IsEnemyAllianceEnabled && npc.Owner is Enemy)
                        continue;

                    _cachedTargets.Add(npc);
                }
                else if ((entity is SmallPlayer smallPlayer))
                {
                    if (_stats.IsEnemyAllianceEnabled)
                        continue;

                    _cachedTargets.Add(smallPlayer);
                }
            }
        }

        private void EvaluateThreat()
        {
            _threatToFlee = null;

            List<IAttacker> threats = _cachedAttackers
                .Where(a => a.RecruitsCount >= RecruitsCount)
                .ToList();

            if (threats.Count == 0)
                return;

            float minDist = float.MaxValue;
            Vector2 myPos = Transform.position;

            foreach (IAttacker threat in threats)
            {
                if (threat.Transform == null)
                    continue;

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

            foreach (IEntity target in _cachedTargets)
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

            foreach (IEntity target in _cachedTargets)
            {
                if (target == null || target.Transform == null)
                    continue;

                float dist = Vector2.Distance(myPos, target.Transform.position);

                if (dist < minDist && dist <= _stats.VisibleDistance)
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
            if (_recruits.Contains(npc) == false)
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

        private void OnDrawGizmosSelected()
        {
            if (_stats == null)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _stats.VisibleDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _stats.AttackDistance);
        }
    }
}