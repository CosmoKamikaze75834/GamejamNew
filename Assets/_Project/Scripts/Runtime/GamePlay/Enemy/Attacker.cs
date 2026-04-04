using System.Collections;
using UnityEngine;

//захватывает людеей
public class Attacker : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;   
    [SerializeField] private Bullet _bulletPrefab;   
    [SerializeField] private float _bulletForce = 10f; 
    [SerializeField] private float _delay = 1f;

    private bool _canShoot = true;

    public void TryShoot(Transform target)
    {
        if (_canShoot == false || target == null)
            return;

        Vector2 direction = (target.position - _firePoint.position).normalized;

        _firePoint.up = direction;

        Shoot();

        StartCoroutine(TimeShoot());
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        if (bullet.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.linearVelocity = _firePoint.up * _bulletForce;
        }

        _canShoot = false;
    }

    private IEnumerator TimeShoot()
    {
        yield return new WaitForSeconds(_delay);
        _canShoot = true;
    }
}