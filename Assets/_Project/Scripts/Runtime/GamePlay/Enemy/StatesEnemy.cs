using UnityEngine;

public class StatesEnemy : MonoBehaviour
{
    [SerializeField] private MoveEnemy _moveEnemy;
    [SerializeField] private Invader _invaider;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Stalker _stalker;
    [SerializeField] private EnemyVision _enemyVision;
    [SerializeField] private float _chaseSpeed = 3f;

    private Transform _currentTarget;

    private void Update()
    {
        if(_currentTarget == null)
        {
            _currentTarget = _enemyVision.FindObjects();
        }

        if (_currentTarget == null)
        {
            Debug.Log("Цели нет");
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
                DisableWander();
                _stalker.MoveToTarget(_currentTarget, _chaseSpeed);
            }
        }
    }

    public void ResetTarger()
    {
        _currentTarget = null;
        EnableWander();
    }

    private void EnableWander()
    {
        if(_moveEnemy.enabled == false)
        {
            _moveEnemy.enabled = true;
        }
    }

    private void DisableWander()
    {
        if (_moveEnemy.enabled == true)
        {
            _moveEnemy.enabled = false;
        }
    }
}