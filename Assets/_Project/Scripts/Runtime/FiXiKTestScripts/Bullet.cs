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

            _rigidbody.linearVelocity = direction.normalized * _speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Destroy(gameObject, _lifeTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out Npc npc))
                npc.Recruit(_attacker);

            Destroy(gameObject);
        }
    }
}