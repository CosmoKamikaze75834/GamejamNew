using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class NpcFactory : MonoBehaviour
    {
        [SerializeField] private Npc _prefab;
        [SerializeField] private WandererStatsConfig _wandererStatsConfig;   
        [SerializeField] private FleeBehaviourStatsConfig _fleeBehaviourStatsConfig;   
        [SerializeField] private Transform _minPositionMap;
        [SerializeField] private Transform _maxPositionMap;


        public event Action<Npc> NpcCreated;

        private float GetRandomX => UnityEngine.Random.Range(_minPositionMap.position.x, _maxPositionMap.position.x);

        private float GetRandomY => UnityEngine.Random.Range(_minPositionMap.position.y, _maxPositionMap.position.y);

        public void Spawn(int count)
        {
            Color color = _prefab.GetComponent<CharacterView>().Color;

            for (int i = 0; i < count; i++)
            {
                Vector3 position = new(GetRandomX, GetRandomY, 0);
                Quaternion rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));

                Npc npc = Instantiate(_prefab, position, rotation);
                npc.Init(_wandererStatsConfig.WandererStats, _fleeBehaviourStatsConfig.FleeBehaviourStats);
                npc.SetColor(color);
                NpcCreated?.Invoke(npc);
            }
        }
    }
}