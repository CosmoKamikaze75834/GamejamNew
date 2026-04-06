using System;
using System.Collections.Generic;

namespace FiXiKTestScripts
{
    public class AttackerRegistry : IDisposable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly EnemyFactory _enemyFactory;

        private readonly List<IAttacker> _attackers = new();

        public event Action<IAttacker> AttackerAdded;

        public AttackerRegistry(PlayerFactory playerFactory, EnemyFactory enemyFactory)
        {
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;

            _playerFactory.PlayerCreated += OnCreated;
            _enemyFactory.EnemyCreated += OnCreated;
        }

        public int Count => _attackers.Count;

        public IReadOnlyList<IAttacker> Attackers => _attackers;

        public void Dispose()
        {
            if (_playerFactory != null)
                _playerFactory.PlayerCreated -= OnCreated;
        }

        private void OnCreated(IAttacker attacker)
        {
            _attackers.Add(attacker);
            AttackerAdded?.Invoke(attacker);
        }
    }
}