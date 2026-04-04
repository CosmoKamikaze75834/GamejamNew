using UnityEngine;

//перследует человека
public class Stalker : MonoBehaviour
{
    public void MoveToTarget(Transform person, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, person.position, speed * Time.deltaTime);
    }
}