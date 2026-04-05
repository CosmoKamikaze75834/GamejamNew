using UnityEngine;

namespace FiXiKTestScripts
{
    public class NpcFactory : MonoBehaviour
    {
        [SerializeField] private Npc _prefab;
        [SerializeField] private float _centerDeviation = 24;

        private float GetRandom => Random.Range(-_centerDeviation, _centerDeviation);

        public void Spawn(int count)
        {
            Color color = _prefab.GetComponent<CharacterView>().Color;

            for (int i = 0; i < count; i++)
            {
                Vector3 position = new(GetRandom, GetRandom, 0);
                Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                Npc npc = Instantiate(_prefab, position, rotation);
                npc.GetComponent<Character>().SetColor(color);
            }
        }
    }
}