using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FiXiKTestScripts
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private Player _prefab;

        private ShooterFactory _shooterFactory;
        private ConspiracyTheoryFactory _conspiracyTheoryFactory;
        private ColorFactory _colorFactory;
        private IInputReader _inputReader;

        public event Action<Player> PlayerCreated;

        [Inject]
        public void Construct(
            ShooterFactory shooterFactory,
            ConspiracyTheoryFactory conspiracyTheoryFactory,
            ColorFactory colorFactory,
            IInputReader resolver)
        {
            
            _shooterFactory = shooterFactory;
            _conspiracyTheoryFactory = conspiracyTheoryFactory;
            _colorFactory = colorFactory;
            _inputReader = resolver;
        }

        public Player Create(Vector3 position, Quaternion rotation)
        {
            Player player = Instantiate(_prefab, position, rotation);
            player.Init(_inputReader);
            player.SetColor(_colorFactory.Give());
            player.SetShooter(_shooterFactory.Get(player));
            ConspiracyTheory theory = _conspiracyTheoryFactory.Get(player.transform);
            player.SetTeamName(theory.LangData);

            PlayerCreated?.Invoke(player);

            return player;
        }
    }
}