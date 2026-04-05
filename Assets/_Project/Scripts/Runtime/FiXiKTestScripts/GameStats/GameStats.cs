using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class GameStats : MonoBehaviour
    {
        [SerializeField] private LineStatsFactory _lineStatsFactory;

        private List<LineStatsView> _lines;

        public void CreateLines(int count)
        {
            _lines = _lineStatsFactory.Get(count);
            UpdateLines();
        }

        private void UpdateLines()
        {
            foreach (var line in _lines)
                UpdateLine(line);
        }

        private void UpdateLine(LineStatsView line)
        {
            line.UpdateStats(
                1,
                "Название теории заговора",
                49,
                80,
                Color.green);
        }
    }
}