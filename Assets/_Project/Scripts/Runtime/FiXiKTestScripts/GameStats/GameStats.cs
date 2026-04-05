using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace FiXiKTestScripts
{
    public class GameStats : MonoBehaviour
    {
        [SerializeField] private LineStatsFactory _lineStatsFactory;

        private List<LineStatsView> _lines;
        private AttackerRegistry _attackerRegistry;
        private NpcRegistry _npcRegistry;

        [Inject]
        public void Construct(AttackerRegistry attackerRegistry, NpcRegistry pcRegistry)
        {
            _attackerRegistry = attackerRegistry;
            _npcRegistry = pcRegistry;
        }

        public void CreateLines()
        {
            _lines = _lineStatsFactory.Get(_attackerRegistry.Count);
            UpdateLines();
        }

        private void UpdateLines()
        {
            int npcTotalCount = _npcRegistry.Count;

            for (int i = 0; i < _lines.Count; i++)
                UpdateLine(i, _lines[i], _attackerRegistry.Attackers[i], npcTotalCount);
        }

        private void UpdateLine(int index, LineStatsView line, IAttacker attacker, int npcTotalCount)
        {
            line.UpdateStats(
                index,
                "Название теории заговора",
                attacker.RecruitsCount,
                npcTotalCount,
                attacker.Color);
        }
    }
}