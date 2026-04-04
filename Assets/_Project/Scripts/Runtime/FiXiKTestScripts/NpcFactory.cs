using UnityEngine;

namespace FiXiKTestScripts
{
    public class NpcFactory : MonoBehaviour
    {
        [SerializeField] private Npc _prefab;
        [SerializeField] private float _centerDeviation = 25;
        [SerializeField] private int _count = 50;

        private float GetRandom => Random.Range(-_centerDeviation, _centerDeviation);

        private void Start() =>
            Spawn();

        private void Spawn()
        {
            for (int i = 0; i < _count; i++)
            {
                Vector3 position = new(GetRandom, GetRandom, 0);
                Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                Npc npc = Instantiate(_prefab, position, rotation);
                npc.Init();
            }
        }
    }
}
