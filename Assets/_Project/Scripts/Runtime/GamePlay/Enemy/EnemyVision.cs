using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float _stoppingDistance = 10f;
    [SerializeField] private float _attackDistance = 2f;

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
        return direction.sqrMagnitude <= _stoppingDistance * _stoppingDistance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stoppingDistance);
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}