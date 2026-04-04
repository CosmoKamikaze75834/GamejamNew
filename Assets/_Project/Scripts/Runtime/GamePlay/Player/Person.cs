using UnityEngine;

public class Person: MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stopdDstance = 2;

    private Transform _target;

    private bool _isChasing = false;

    public bool IsChasing => _isChasing;

    private void Update()
    {
        if (_isChasing == true && _target != null)
        {
            float distance = Vector2.Distance(transform.position, _target.position);

            if(distance > _stopdDstance)
            {
                ChangeRoute();
            }
        }
    }

    public void ChangeRoute()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    public void StartChasing(Transform target)
    {
        _target = target;
        _isChasing = true;
    }
}