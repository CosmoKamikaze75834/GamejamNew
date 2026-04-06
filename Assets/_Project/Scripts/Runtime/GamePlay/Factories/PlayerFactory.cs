using System;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace FiXiKTestScripts
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private WandererStatsConfig _wandererStatsConfig;
        [SerializeField] private FleeBehaviourStatsConfig _fleeBehaviourStatsConfig;
        [SerializeField] private PlayerBroadcaster _playerBroadcasterPrefab;
        [SerializeField] private SmallPlayer _smallPlayerPrefab;

        private ShooterFactory _shooterFactory;
        private ConspiracyTheoryFactory _conspiracyTheoryFactory;
        private ColorFactory _colorFactory;
        private CinemachineCamera _cinemachineCamera;
        private IInputReader _inputReader;

        public event Action<PlayerBroadcaster> PlayerCreated;

        [Inject]
        public void Construct(
            ShooterFactory shooterFactory,
            ConspiracyTheoryFactory conspiracyTheoryFactory,
            ColorFactory colorFactory,
            IInputReader resolver,
            CinemachineCamera cinemachineCamera)
        {

            _shooterFactory = shooterFactory;
            _conspiracyTheoryFactory = conspiracyTheoryFactory;
            _colorFactory = colorFactory;
            _inputReader = resolver;
            _cinemachineCamera = cinemachineCamera;
        }

        public PlayerBroadcaster CreatePlayerBroadcaster(SmallPlayer smallPlayer)
        {
            PlayerBroadcaster player = Instantiate(
                _playerBroadcasterPrefab, 
                smallPlayer.Transform.position, 
                smallPlayer.Transform.rotation);

            player.Init(_inputReader);
            player.SetColor(smallPlayer.Color);
            player.SetShooter(_shooterFactory.Get(player, 0.5f));
            ConspiracyTheory theory = _conspiracyTheoryFactory.Get(player.transform);
            player.SetTeamName(theory.LangData);
            _cinemachineCamera.Follow = player.transform;
            smallPlayer.Destroy();

            PlayerCreated?.Invoke(player);

            return player;
        }

        public SmallPlayer CreateSmallPlayer(Vector3 position, Quaternion rotation)
        {
            SmallPlayer player = Instantiate(_smallPlayerPrefab, position, rotation);
            
            player.Init(_inputReader, 
                _wandererStatsConfig.WandererStats, 
                _fleeBehaviourStatsConfig.FleeBehaviourStats);
            
            player.SetColor(_colorFactory.Give());            
            _cinemachineCamera.Follow = player.transform;

            return player;
        }
    }
}