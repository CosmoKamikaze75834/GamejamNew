using System;
using UnityEngine;
using VContainer;

namespace FiXiKTestScripts
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private EnemyStatsConfig _easyEnemyStatsConfig;
        [SerializeField] private EnemyStatsConfig _normalEnemyStatsConfig;
        [SerializeField] private EnemyStatsConfig _hardEnemyStatsConfig;
        [SerializeField] private EnemyStatsConfig _madnessEnemyStatsConfig;
        [SerializeField] private WandererStatsConfig _wandererStatsConfig;
        [SerializeField] private float _centerDeviation = 24;

        private ShooterFactory _shooterFactory;
        private ConspiracyTheoryFactory _conspiracyTheoryFactory;
        private ColorFactory _colorFactory;

        public event Action<Enemy> EnemyCreated;

        private float GetRandom => UnityEngine.Random.Range(-_centerDeviation, _centerDeviation);

        [Inject]
        public void Construct(
            ShooterFactory shooterFactory,
            ConspiracyTheoryFactory conspiracyTheoryFactory,
            ColorFactory colorFactory)
        {

            _shooterFactory = shooterFactory;
            _conspiracyTheoryFactory = conspiracyTheoryFactory;
            _colorFactory = colorFactory;
        }

        public void Spawn(int count)
        {
            for (int i = 0; i < count; i++)
            {
                EnemyStats stats = GetActualConfig().Stats;

                Vector3 position = new(GetRandom, GetRandom, 0);
                Quaternion rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

                Enemy enemy = Instantiate(_prefab, position, rotation);
                enemy.Init(stats, _wandererStatsConfig.WandererStats);
                enemy.SetColor(_colorFactory.Give());

                Shooter shooter = _shooterFactory.Get(enemy);
                shooter.SetReloadTime(stats.ReloadTime);
                enemy.SetShooter(shooter);

                ConspiracyTheory theory = _conspiracyTheoryFactory.Get(enemy.transform);
                enemy.SetTeamName(theory.LangData);

                EnemyCreated?.Invoke(enemy);
            }
        }

        private EnemyStatsConfig GetActualConfig()
        {
            return DifficultySwitcher.Difficulty switch
            {
                DifficultyType.Easy => _easyEnemyStatsConfig,
                DifficultyType.Normal => _normalEnemyStatsConfig,
                DifficultyType.Hard => _hardEnemyStatsConfig,
                DifficultyType.Madness => _madnessEnemyStatsConfig,
                _ => throw new Exception($"Указанный тип {nameof(DifficultyType)} {DifficultySwitcher.Difficulty} не обработан"),
            };
        }
    }
}