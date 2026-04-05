using UnityEngine;
using VContainer;

namespace FiXiKTestScripts
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private float _centerDeviation = 24;

        private ShooterFactory _shooterFactory;
        private ConspiracyTheoryFactory _conspiracyTheoryFactory;
        private ColorFactory _colorFactory;

        private float GetRandom => Random.Range(-_centerDeviation, _centerDeviation);

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
                Vector3 position = new(GetRandom, GetRandom, 0);
                Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                Enemy enemy = Instantiate(_prefab, position, rotation);
                Shooter shooter = _shooterFactory.Get(enemy);
                enemy.SetShooter(shooter);
                enemy.GetComponent<Character>().SetColor(_colorFactory.Give());

                ConspiracyTheory theory = _conspiracyTheoryFactory.Get(enemy.transform);
            }
        }
    }
}