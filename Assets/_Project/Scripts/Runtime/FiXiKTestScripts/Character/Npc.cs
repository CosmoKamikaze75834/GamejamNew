using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class Npc : MonoBehaviour, IEntity
    {
        [SerializeField] private Character _character;
        [SerializeField] private Wanderer _wanderer;
        [SerializeField] private FleeBehavior _fleeBehavior;

        public IAttacker Owner { get; private set; }

        public Transform Transform { get; private set; }

        public Color Color => _character.Color;

        private void Awake()
        {
            Transform = transform;

            if (_fleeBehavior != null)
                _fleeBehavior.SetOwner(Owner);
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.deltaTime;

            if (_fleeBehavior != null && _fleeBehavior.UpdateFlee(deltaTime, out _))
                return;

            if (Owner != null)
            {
                _character.RotateTo(Owner.Transform.position, deltaTime);
                _character.MoveTo(Owner.Transform.position, deltaTime * 6);

                if (_fleeBehavior != null)
                    _fleeBehavior.ResetSpeed();
            }
            else
            {
                _wanderer.UpdateWander(deltaTime);
            }
        }

        public void Recruit(IAttacker attacker)
        {
            if (Owner != null && Owner != attacker)
                Owner.RemoveRecruit(this);

            _character.SetColor(attacker.Color);
            Owner = attacker;
            attacker.AddRecruit(this);

            if (_fleeBehavior != null)
                _fleeBehavior.SetOwner(attacker);
        }
    }
}