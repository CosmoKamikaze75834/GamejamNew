using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class FleeBehavior
    {
        private readonly Character _character;
        private readonly Transform _transform;
        private readonly FleeBehaviourStats _stats;
        private readonly float _originalSpeed;

        private IAttacker _owner;
        private float _lastScanTime;
        private IAttacker _nearestThreat;

        public FleeBehavior(Character character, FleeBehaviourStats stats)
        {
            _character = character;
            _transform = character.transform;
            _stats = stats;
            _originalSpeed = _character.Speed;
        }

        public void SetOwner(IAttacker owner) => _owner = owner;

        public bool UpdateFlee(float deltaTime, out Vector2 fleeDirection)
        {
            fleeDirection = Vector2.zero;

            if (_character == null)
                return false;

            if (Time.time >= _lastScanTime + _stats.ScanInterval)
            {
                ScanForThreats();
                _lastScanTime = Time.time;
            }

            if (_nearestThreat == null || _nearestThreat.Transform == null)
                return false;

            Vector2 myPos = _transform.position;
            Vector2 threatPos = _nearestThreat.Transform.position;
            Vector2 away = (myPos - threatPos).normalized;

            _character.Move(away);
            _character.Rotate(away, deltaTime);
            _character.SetSpeed(_originalSpeed * _stats.FleeSpeedMultiplier);

            fleeDirection = away;

            return true;
        }

        private void ScanForThreats()
        {
            List<IEntity> allEntities = Scanner.Scan(_transform.position, _stats.DetectionRadius, _stats.TargetLayers);

            List<IAttacker> threats = allEntities
                .Where(e => e is IAttacker attacker && attacker != _owner)
                .Select(e => e as IAttacker)
                .Where(a => a != null)
                .ToList();

            _nearestThreat = null;
            float minDist = float.MaxValue;
            Vector2 myPos = _transform.position;

            foreach (var threat in threats)
            {
                if (threat.Transform == null) 
                    continue;

                float dist = Vector2.Distance(myPos, threat.Transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    _nearestThreat = threat;
                }
            }
        }

        public void ResetSpeed() =>
            _character.SetSpeed(_originalSpeed);
    }
}