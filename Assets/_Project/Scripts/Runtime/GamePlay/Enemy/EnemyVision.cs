using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float _stoppingDistance = 10f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private LayerMask _layerMasck;

    public Transform FindObjects()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _stoppingDistance, _layerMasck);

        foreach (Collider2D hit in hits)
        {
            if(hit.TryGetComponent<Person>(out var person))
            {
                if(person.IsChasing == false)
                {
                    return hit.transform;
                }
            }
        }

        return null;
    }

    public bool IsTargetDetected(Transform target)
    {
        if (target == null)
            return false;

        Vector2 direction = transform.position - target.position;
        return direction.sqrMagnitude < _stoppingDistance * _stoppingDistance;
    }

    public bool IsTargetAttackRange(Transform target)
    {
        if (target == null)
            return false;

        Vector2 direction = transform.position - target.position;
        return direction.sqrMagnitude <= _attackDistance * _attackDistance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stoppingDistance);
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}