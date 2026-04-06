using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    public class SmallEnemy : MonoBehaviour, IEntity
    {
        [SerializeField] private float _originalSpeed = 4;
        [SerializeField] private float _seekRadioRadius = 10f;
        [SerializeField] private float _seekRadioInterval = 0.5f;

        private Character _character;
        private FleeBehavior _fleeBehavior;
        private Wanderer _wanderer;
        private Transform _targetRadio;
        private float _lastSeekTime;

        private Npc _npc;

        public event Action<SmallEnemy> Recruted;

        public IAttacker Owner { get; private set; }

        public Transform Transform { get; private set; }

        public Color Color => _character.Color;

        public void Init(WandererStats wandererStats, FleeBehaviourStats fleeStats)
        {
            _npc = GetComponent<Npc>();
            _npc.Init(wandererStats, fleeStats);
            _character = GetComponent<Character>();
            _character.Init(_originalSpeed);
            Transform = transform;
            _wanderer = new(_character, wandererStats);
            _fleeBehavior = new(_character, fleeStats);
            _fleeBehavior.SetOwner(Owner);
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.deltaTime;

            if (TrySeekRadio(deltaTime))
                return;

            if (_fleeBehavior.UpdateFlee(deltaTime, out _))
                return;

            _wanderer.UpdateWander(deltaTime);
        }

        public void Destroy() =>
            Destroy(gameObject);

        public void InvokeRecruted() =>
            Recruted?.Invoke(this);

        public void SetColor(Color color) =>
            _character.SetColor(color);

        public Npc ConvertToNpc()
        {
            GetComponent<Rigidbody2D>().mass = 1;
            enabled = false;
            _npc.enabled = true;

            return _npc;
        }

        private bool TrySeekRadio(float deltaTime)
        {
            if (Time.time < _lastSeekTime + _seekRadioInterval)
            {
                if (_targetRadio != null)
                {
                    Vector2 direction = (_targetRadio.position - Transform.position).normalized;
                    _character.Move(direction);
                    _character.Rotate(direction, deltaTime);
                    _character.SetSpeed(_originalSpeed);

                    return true;
                }

                return false;
            }

            _lastSeekTime = Time.time;
            List<Radio> allEntities = NewMethod();
            Radio radioEntity = null;

            foreach (var entity in allEntities)
            {

                if (entity is Radio)
                {
                    radioEntity = entity;

                    break;
                }
            }

            if (radioEntity != null && radioEntity.transform != null)
            {
                _targetRadio = radioEntity.transform;
                Vector2 direction = (_targetRadio.position - Transform.position).normalized;
                _character.Move(direction);
                _character.Rotate(direction, deltaTime);
                _character.SetSpeed(_originalSpeed);

                return true;
            }
            else
            {
                _targetRadio = null;

                return false;
            }
        }

        private List<Radio> NewMethod()
        {
            return Scanner.Scan<Radio>(Transform.position, _seekRadioRadius, Physics2D.AllLayers);
        }
    }
}