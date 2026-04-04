using System;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public event Action<Person> Entered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Person>(out var person))
        {
            Entered?.Invoke(person);
            Destroy(gameObject);
        }
    }
}