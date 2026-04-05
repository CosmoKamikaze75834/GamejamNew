using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Character))]
    public class Npc : MonoBehaviour, IEntity
    {
        [SerializeField] private Character _character;
        [SerializeField] private FleeBehavior _fleeBehavior;

        [Header("Following Settings")]
        [SerializeField] private float _maxFollowSpeed = 60f;
        [SerializeField] private float _maxFollowDistance = 10f;

        private Wanderer _wanderer;

        public IAttacker Owner { get; private set; }

        public Transform Transform { get; private set; }

        public Color Color => _character.Color;

        public void Init(WandererStats stats)
        {
            Transform = transform;
            _wanderer = new(_character, stats);

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
                Vector2 ownerPos = Owner.Transform.position;
                float distance = Vector2.Distance(Transform.position, ownerPos);

                float t = Mathf.Clamp01(distance / _maxFollowDistance);
                float currentSpeed = Mathf.Lerp(0f, _maxFollowSpeed, t);
                _character.SetSpeed(currentSpeed);

                _character.RotateTo(ownerPos, deltaTime);
                _character.MoveTo(ownerPos, deltaTime);

                if (_fleeBehavior != null)
                    _fleeBehavior.ResetSpeed();
            }
            else
            {
                _wanderer.UpdateWander(deltaTime);
            }
        }

        public void SetColor(Color color) =>
            _character.SetColor(color);

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