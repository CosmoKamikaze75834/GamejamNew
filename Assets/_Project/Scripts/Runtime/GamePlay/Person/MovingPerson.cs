using System.Collections;
using UnityEngine;

public class MovingPerson : MonoBehaviour
{
    [SerializeField] private Person _person;
    [SerializeField] private float _speed;
    [SerializeField] private float _changeDirectionTime = 2f;

    private Vector2 _moveDirection;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if(_person.IsChasing == true)
        {
            return;
        }

        SetRandomDirection();
        StartCoroutine(ChangeDirectionRoutine());
    }

    private void FixedUpdate()
    {
        if (_person.IsChasing == true)
        {
            return;
        }

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