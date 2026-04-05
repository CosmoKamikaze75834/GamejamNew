using System;
using UnityEngine;
using VContainer;

namespace FiXiKTestScripts
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private EnemyStatsConfig _enemyStatsConfig;
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
                EnemyStats stats = _enemyStatsConfig.Stats;

                Vector3 position = new(GetRandom, GetRandom, 0);
                Quaternion rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

                Enemy enemy = Instantiate(_prefab, position, rotation);
                enemy.SetStats(stats);
                Shooter shooter = _shooterFactory.Get(enemy);
                shooter.SetReloadTime(stats.ReloadTime);
                enemy.SetShooter(shooter);

                Character character = enemy.GetComponent<Character>();

                character.SetColor(_colorFactory.Give());
                character.SetSpeed(stats.MovementSpeed);

                ConspiracyTheory theory = _conspiracyTheoryFactory.Get(enemy.transform);
                enemy.SetTeamName(theory.LangData);

                EnemyCreated?.Invoke(enemy);
            }
        }
    }
}