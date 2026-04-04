using System.Collections;
using UnityEngine;

public class Shooting: MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletForce = 20f;
    [SerializeField] private float _delay = 3f;

    private bool _canShoot = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_canShoot == true)
            {
                Debug.Log("Shoot");
                Shoot();
                _canShoot = false;
                StartCoroutine(TimeShoot());
            }
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.Entered += OnPersonHit;

        if(bullet.TryGetComponent(out Rigidbody2D rb))
        {
            rb.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);
        }
    }

    private void OnPersonHit(Person person)
    {
        person.StartChasing(transform);
    }

    private IEnumerator TimeShoot()
    {
        yield return new WaitForSeconds(_delay);
        _canShoot = true;
    }
}