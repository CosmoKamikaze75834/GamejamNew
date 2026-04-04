using System.Collections;
using UnityEngine;

public class MovingPerson : MonoBehaviour
{
    [SerializeField] private Person _person;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _changeDirectionTime = 0.5f;

    private Vector2 _moveDirection;
    private Rigidbody2D _rb;
    private Coroutine _changeDirectionRoutine;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Debug.Log($"{name} IsChasing at start: {_person.IsChasing}");

        // Если человек уже следует за кем-то — хаотичное движение не запускаем
        if (_person.IsChasing)
            return;

        SetRandomDirection();
        _changeDirectionRoutine = StartCoroutine(ChangeDirectionRoutine());
    }

    private void FixedUpdate()
    {
        // Если человек начал следовать за целью — останавливаем хаотичное движение
        if (_person.IsChasing)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        _rb.linearVelocity = _moveDirection * _speed;
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_changeDirectionTime);

            // Если человек уже начал follow — выходим из корутины
            if (_person.IsChasing)
            {
                _rb.linearVelocity = Vector2.zero;
                yield break;
            }

            SetRandomDirection();
        }
    }

    private void SetRandomDirection()
    {
        Vector2 dir = Random.insideUnitCircle;

        if (dir.sqrMagnitude < 0.01f)
            dir = Vector2.right;

        _moveDirection = dir.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Если врезались во что-то, сразу меняем направление
        SetRandomDirection();
    }
}