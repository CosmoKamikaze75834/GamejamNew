using System;
using UnityEngine;
using VContainer;

namespace FiXiKTestScripts
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private SmallEnemy _smallEnemyPrefab;
        [SerializeField] private EnemyStatsConfig _easyEnemyStatsConfig;
        [SerializeField] private EnemyStatsConfig _normalEnemyStatsConfig;
        [SerializeField] private EnemyStatsConfig _hardEnemyStatsConfig;
        [SerializeField] private EnemyStatsConfig _madnessEnemyStatsConfig;
        [SerializeField] private WandererStatsConfig _wandererStatsConfig;
        [SerializeField] private WandererStatsConfig _npcWandererStatsConfig;
        [SerializeField] private FleeBehaviourStatsConfig _npcFleeBehaviourStatsConfig;
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
                SpawnSmallEnemy();
        }

        public void SpawnSmallEnemy()
        {
            EnemyStats stats = GetActualConfig().Stats;

            Vector3 position = new(GetRandom, GetRandom, 0);
            Quaternion rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

            SmallEnemy smallEnemy = Instantiate(_smallEnemyPrefab, position, rotation);
            smallEnemy.Init(_npcWandererStatsConfig.WandererStats, _npcFleeBehaviourStatsConfig.FleeBehaviourStats);
            smallEnemy.SetColor(_colorFactory.Give());

            smallEnemy.Recruted += OnSmallEnemyDestroyed;
        }

        public void RespawnSmallEnemy(SmallEnemy destroyedEnemy)
        {
            EnemyStats stats = GetActualConfig().Stats;

            Vector3 position = new(GetRandom, GetRandom, 0);
            Quaternion rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

            SmallEnemy smallEnemy = Instantiate(_smallEnemyPrefab, position, rotation);
            smallEnemy.Init(_npcWandererStatsConfig.WandererStats, _npcFleeBehaviourStatsConfig.FleeBehaviourStats);
            smallEnemy.SetColor(destroyedEnemy.Color);

            smallEnemy.Recruted += OnSmallEnemyDestroyed;
        }

        public void CreateEnemyBroadcaster(SmallEnemy smallEnemy)
        {
            EnemyStats stats = GetActualConfig().Stats;

            Enemy enemy = Instantiate(
                _prefab, 
                smallEnemy.transform.position, 
                smallEnemy.transform.rotation);

            enemy.Init(stats, _wandererStatsConfig.WandererStats);
            enemy.SetColor(smallEnemy.Color);
            Shooter shooter = _shooterFactory.Get(enemy, stats.ReloadTime);
            enemy.SetShooter(shooter);
            ConspiracyTheory theory = _conspiracyTheoryFactory.Get(enemy.transform);
            enemy.SetTeamName(theory.LangData);
            smallEnemy.Destroy();

            EnemyCreated?.Invoke(enemy);
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

        private void OnSmallEnemyDestroyed(SmallEnemy enemy)
        {
            enemy.Recruted -= OnSmallEnemyDestroyed;
            RespawnSmallEnemy(enemy);
        }
    }
}