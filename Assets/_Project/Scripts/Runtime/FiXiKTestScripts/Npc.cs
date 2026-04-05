using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class Npc : MonoBehaviour, IEntity
    {
        [SerializeField] private Character _character;
        [SerializeField] private Wanderer _wanderer;

        public IAttacker Owner { get; private set; }

        public Transform Transform { get; private set; }

        public Color Color => _character.Color;

        private void Awake() =>
            Transform = transform;

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            if (Owner != null)
            {
                _character.RotateTo(Owner.Transform.position, deltaTime);
                _character.MoveTo(Owner.Transform.position, deltaTime * 6);
            }
            else
            {
                _wanderer.UpdateWander(deltaTime);
            }
        }

        public void Recruit(IAttacker attacker)
        {
            _character.SetColor(attacker.Color);
            Owner = attacker;
        }
    }
}