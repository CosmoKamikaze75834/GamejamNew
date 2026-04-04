using UnityEngine;

public class Person: MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;

    private bool _isChasing = false;

    private void Update()
    {
        if (_isChasing == true)
        {
            ChangeRoute();
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