using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    [Serializable]
    public class FleeBehaviourStats
    {
        [SerializeField] private LayerMask _targetLayers;
        [SerializeField] private float _detectionRadius = 6f;
        [SerializeField] private float _fleeSpeedMultiplier = 1.5f;
        [SerializeField] private float _scanInterval = 0.3f;

        public LayerMask TargetLayers => _targetLayers;

        public float DetectionRadius => _detectionRadius;

        public float FleeSpeedMultiplier => _fleeSpeedMultiplier;

        public float ScanInterval => _scanInterval;
    }
}