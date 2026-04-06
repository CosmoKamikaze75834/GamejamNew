using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class RadioMediator : MonoBehaviour
    {
        [SerializeField] private RadioFactory _radioFactory;
        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private EnemyFactory _enemyFactory;

        private readonly List<Radio> _radioList = new();

        private void OnEnable()
        {
            _radioFactory.Created += OnRadioCreated;
        }

        private void OnDisable()
        {
            _radioFactory.Created -= OnRadioCreated;
        }

        private void OnRadioCreated(Radio radio)
        {
            _radioList.Add(radio);
            radio.Entered += OnAttackerEntered;
        }

        private void OnAttackerEntered(Radio radio, IEntity entity)
        {
            if (entity is SmallPlayer smallPlayer)
            {
                radio.Entered -= OnAttackerEntered;
                _radioList.Remove(radio);
                radio.Destroy();
                _playerFactory.CreatePlayerBroadcaster(smallPlayer);
            }
            else if (entity is SmallEnemy smallEnemy)
            {
                radio.Entered -= OnAttackerEntered;
                _radioList.Remove(radio);
                radio.Destroy();
                _enemyFactory.CreateEnemyBroadcaster(smallEnemy);
            }
        }
    }
}