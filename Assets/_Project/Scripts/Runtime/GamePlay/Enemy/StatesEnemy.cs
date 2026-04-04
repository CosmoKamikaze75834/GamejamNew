using UnityEngine;

public class StatesEnemy : MonoBehaviour
{
    [SerializeField] private MoveEnemy _moveEnemy;
    [SerializeField] private Invader _invaider;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Stalker _stalker;
    [SerializeField] private EnemyVision _enemyVision;
    [SerializeField] private float _chaseSpeed = 3f;

    [SerializeField] private Transform _currentTarget;


    private void Update()
    {
        if(_currentTarget == null)
        {
            _moveEnemy.enabled = true;
            return;
        }

        if (_enemyVision.IsTargetDetected(_currentTarget))
        {
            Debug.Log("Заметили человека");

            _moveEnemy.enabled = false;//выключили движение
            Debug.Log("отключили хаотичное движение");

            if (_enemyVision.IsTargetAttackRange(_currentTarget))
            {
                Debug.Log("человек в радиусе атаки");
                _attacker.TryShoot(_currentTarget);
            }
            else
            {
                Debug.Log("преследуем человека");
                _stalker.MoveToTarget(_currentTarget, _chaseSpeed);
            }
        }
    }
}