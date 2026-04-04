using UnityEngine;

public class Person: MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance = 2f;

    private Transform _target;
    private Rigidbody2D _rb;
    private bool _isChasing = false;

    public bool IsChasing => _isChasing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isChasing && _target != null)
        {
            float distance = Vector2.Distance(_rb.position, (Vector2)_target.position);

            if (distance > _stopDistance)
            {
                Vector2 newPos = Vector2.MoveTowards(_rb.position, _target.position, _speed * Time.fixedDeltaTime);
                _rb.MovePosition(newPos);
            }
            else
            {
                _rb.linearVelocity = Vector2.zero;
            }
        }
    }

    public void StartChasing(Transform target)
    {
        _target = target;
        _isChasing = true;
    }
}