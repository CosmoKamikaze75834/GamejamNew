using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace FiXiKTestScripts
{
    public class GameStats : MonoBehaviour
    {
        [SerializeField] private LineStatsFactory _lineStatsFactory;
        [SerializeField] private int _maximumLines = 5;

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
            _lines = _lineStatsFactory.Get(_maximumLines);
            UpdateLines();

            _attackerRegistry.AttackerAdded += OnAttackerAdded;

            foreach (IAttacker attacker in _attackerRegistry.Attackers)
                attacker.CountChanged += OnCountChanged;
        }

        private void OnDestroy()
        {
            if (_attackerRegistry != null)
                _attackerRegistry.AttackerAdded -= OnAttackerAdded;
        }

        private void OnAttackerAdded(IAttacker attacker)
        {
            attacker.CountChanged += OnCountChanged;
            UpdateLines();
        }

        private void UpdateLines()
        {
            int npcTotalCount = _npcRegistry.Count;

            List<IAttacker> sortedAttackers = _attackerRegistry.Attackers
                .OrderByDescending(a => a.RecruitsCount)
                .ToList();

            for (int i = 0; i < _lines.Count; i++)
            {
                if (i < sortedAttackers.Count)
                {
                    _lines[i].gameObject.SetActive(true);
                    UpdateLine(i + 1, _lines[i], sortedAttackers[i], npcTotalCount);
                }
                else
                {
                    _lines[i].gameObject.SetActive(false);
                }
            }
        }

        private void UpdateLine(int index, LineStatsView line, IAttacker attacker, int npcTotalCount)
        {
            line.UpdateStats(
                index,
                attacker.TeamName,
                attacker.RecruitsCount,
                npcTotalCount,
                attacker.Color);
        }

        private void OnCountChanged() => UpdateLines();
    }
}