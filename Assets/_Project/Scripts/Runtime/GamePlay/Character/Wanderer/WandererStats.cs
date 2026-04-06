using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    [Serializable]
    public class WandererStats
    {
        [Header("Интервал между сменами направления (секунды)")]
        [SerializeField] private float _minChangeInterval = 1f;
        [SerializeField] private float _maxChangeInterval = 5f;

        [Header("Изменение направления в реальном времени (градусы в секунду)")]
        [SerializeField] private float _minDriftSpeed = 10f;
        [SerializeField] private float _maxDriftSpeed = 60f;

        [Header("Диапазон скоростей перемещения")]
        [SerializeField] private float _minSpeed = 2f;
        [SerializeField] private float _maxSpeed = 7f;

        public float MinChangeInterval => _minChangeInterval;

        public float MaxChangeInterval => _maxChangeInterval;

        public float MinDriftSpeed => _minDriftSpeed;

        public float MaxDriftSpeed => _maxDriftSpeed;

        public float MinSpeed => _minSpeed;

        public float MaxSpeed => _maxSpeed;
    }
}