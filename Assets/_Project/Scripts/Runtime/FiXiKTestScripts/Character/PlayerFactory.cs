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
        private IObjectResolver _resolver;

        public event Action<Player> PlayerCreated;

        [Inject]
        public void Construct(
            ShooterFactory shooterFactory,
            ConspiracyTheoryFactory conspiracyTheoryFactory,
            ColorFactory colorFactory,
            IObjectResolver resolver)
        {
            
            _shooterFactory = shooterFactory;
            _conspiracyTheoryFactory = conspiracyTheoryFactory;
            _colorFactory = colorFactory;
            _resolver = resolver;
        }

        public Player Create(Vector3 position, Quaternion rotation)
        {
            Player player = _resolver.Instantiate(_prefab, position, rotation);
            player.SetShooter(_shooterFactory.Get(player));
            player.GetComponent<Character>().SetColor(_colorFactory.Give());
            _conspiracyTheoryFactory.Get(player.transform);
            PlayerCreated?.Invoke(player);

            return player;
        }
    }
}