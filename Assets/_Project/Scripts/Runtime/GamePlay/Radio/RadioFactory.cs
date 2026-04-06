using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class RadioFactory : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spanwPoints;
        [SerializeField] private Radio _prefab;

        public event Action<Radio> Created;

        public void Spawn(int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                Transform spawnPoint = GiveSpawnPoint();
                Radio radio = Instantiate(_prefab, spawnPoint.position, spawnPoint.rotation);

                Created?.Invoke(radio);
            }
        }

        private Transform GiveSpawnPoint()
        {
            int index = UnityEngine.Random.Range(0, _spanwPoints.Count);
            Transform spawnPoint = _spanwPoints[index];
            _spanwPoints.RemoveAt(index);

            return spawnPoint;
        }
    }
}