using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    [Serializable]
    public class EnemyStats
    {
        [SerializeField] private float _visibleDistance = 10f;
        [SerializeField] private float _attackDistance = 5f;
        [SerializeField] private float _scanInterval = 0.5f;
        [SerializeField] private float _fleeSpeedMultiplier = 1.5f;
        [SerializeField] private float _movementSpeed = 4;
        [SerializeField] private float _reloadTime = 2;

        public float VisibleDistance => _visibleDistance;

        public float AttackDistance => _attackDistance;

        public float ScanInterval => _scanInterval;

        public float FleeSpeedMultiplier => _fleeSpeedMultiplier;

        public float MovementSpeed => _movementSpeed;

        public float ReloadTime => _reloadTime;
    }
}