using UnityEngine;

namespace FiXiKTestScripts
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private TEMP_EntryPointGame _entry;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private ConspiracyTheory _conspiracyTheoryPrefab;
        [SerializeField] private float _centerDeviation = 24;
        [SerializeField] private int _count = 7;

        private float GetRandom => Random.Range(-_centerDeviation, _centerDeviation);

        private void Start() =>
            Spawn();

        private void Spawn()
        {
            for (int i = 0; i < _count; i++)
            {
                Vector3 position = new(GetRandom, GetRandom, 0);
                Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                Enemy enemy = Instantiate(_prefab, position, rotation);
                Shooter shooter = new(enemy, _bulletPrefab);
                enemy.SetShooter(shooter);
                enemy.GetComponent<Character>().SetColor(_entry.GiveColor());

                ConspiracyTheory conspiracyTheory = Instantiate(_conspiracyTheoryPrefab, enemy.transform.position, Quaternion.identity);
                conspiracyTheory.SetText(_entry.GiveTheory()).SetTarget(enemy.transform);
            }
        }
    }
}