using UnityEngine;

public class Person: MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _stopdDstance = 2;

    private bool _isChasing = false;

    public bool IsChasing => _isChasing;

    private void Update()
    {
        if (_isChasing == true)
        {
            float distance = Vector2.Distance(transform.position, _player.position);

            if(distance > _stopdDstance)
            {
                ChangeRoute();
            }
        }
    }

    public void ChangeRoute()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }

    public void StartChasing()
    {
        _isChasing = true;
    }
}