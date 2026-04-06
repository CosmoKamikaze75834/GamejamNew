using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class LineStatsFactory : MonoBehaviour
    {
        [SerializeField] private LineStatsView _prefab;
        [SerializeField] private Transform _parent;

        public List<LineStatsView> Get(int count)
        {
            List<LineStatsView> lines = new();

            for (int i = 0; i < count; i++)
                lines.Add(Get());

            return lines;
        }

        public LineStatsView Get() =>
            Instantiate(_prefab, _parent);
    }
}