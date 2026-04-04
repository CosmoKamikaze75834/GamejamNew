using System.Collections;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _changeDirectionTime = 2f;

    private Vector2 _moveDirection;
    private Rigidbody2D _rb;
    private Coroutine _coroutine;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SetRandomDirection();
        _coroutine = StartCoroutine(ChangeDirectionRoutine());
    }

    private void OnDisable()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if(_rb != null)
        {
            _rb.linearVelocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDirection * _speed;
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_changeDirectionTime);
            SetRandomDirection();
        }
    }

    private void SetRandomDirection()
    {
        _moveDirection = Random.insideUnitCircle.normalized;
    }
}