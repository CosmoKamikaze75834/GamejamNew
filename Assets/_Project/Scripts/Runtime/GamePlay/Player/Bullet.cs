using System;
using System.Threading;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public event Action<Person> BulletAchieved;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Person person))
        {
            BulletAchieved?.Invoke(person);
            person.StartChasing();
            Destroy(gameObject);
        }
    }
}
