using UnityEngine;

public class Bullet: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out _))
        {
            Destroy(gameObject);
        }
    }
}