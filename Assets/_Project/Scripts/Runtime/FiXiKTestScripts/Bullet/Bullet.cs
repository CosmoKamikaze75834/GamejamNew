using UnityEngine;

namespace FiXiKTestScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;

        private IAttacker _attacker;
        private Rigidbody2D _rigidbody;

        public float Speed => _speed;

        public void Init(IAttacker attaker, Vector2 direction)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _attacker = attaker;

            Vector2 dir = direction.normalized;

            if (dir == Vector2.zero)
            {
                Debug.LogWarning("Bullet: попытка выстрелить с нулевым направлением, пуля уничтожена");
                Destroy(gameObject);

                return;
            }

            _rigidbody.linearVelocity = direction.normalized * _speed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Destroy(gameObject, _lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEntity entity))
            {
                if (_attacker == entity)
                    return;

                if (entity is Npc npc)
                {
                    if (npc.Owner == _attacker)
                        return;

                    npc.Recruit(_attacker);
                }
            }

            Destroy(gameObject);
        }
    }
}